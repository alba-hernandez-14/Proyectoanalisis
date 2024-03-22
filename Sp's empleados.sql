
create table CXC_EMPLEADO(
     EMP_EMPLEADO NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
     EMP_NOMBRE VARCHAR (50),
     EMP_APELLIDO VARCHAR (50),
     EMP_TELEFONO VARCHAR (11),
     EMP_DIRECCION VARCHAR (50),
     EMP_CORREO_ELECTRONICO VARCHAR (50),
     EMP_FECHA_CREACION date default sysdate,
     EMP_FECHA_ELIMINACION date default null,
     constraint empleado_pk primary key (EMP_EMPLEADO)
);

create or replace type t_cxc_empleado_record as object (
     EMP_EMPLEADO NUMBER,
     EMP_NOMBRE VARCHAR (50),
     EMP_APELLIDO VARCHAR (50),
     EMP_TELEFONO VARCHAR (11),
     EMP_DIRECCION VARCHAR (50),
     EMP_CORREO_ELECTRONICO VARCHAR (50),
     EMP_FECHA_CREACION date,
     EMP_FECHA_ELIMINACION date
);

create or replace type  t_cxc_empleado_table as table of  t_cxc_empleado_record;

Create or replace procedure pas_crear_empleado(
     p_nombre CXC_EMPLEADO.EMP_NOMBRE%type,
     p_apellido CXC_EMPLEADO.EMP_APELLIDO%type,
     p_telefono CXC_EMPLEADO.EMP_TELEFONO%type,
     p_direccion CXC_EMPLEADO.EMP_DIRECCION%type,
     p_email CXC_EMPLEADO.EMP_CORREO_ELECTRONICO%type
    )
    is
	v_empleado_encontrado int;
begin
    select 
	   case when exists(select EMP_EMPLEADO from CXC_EMPLEADO where lower(EMP_CORREO_ELECTRONICO) = LOWER(p_email) and EMP_FECHA_ELIMINACION is NULL)
            then 1
            else 0
        end
    into v_empleado_encontrado
    from dual;

   if(v_empleado_encontrado = 1) then
      	raise_application_error(-20000, 'Ya se ha registrado un empleado con ese correo electonico');
    end if;

    insert into CXC_EMPLEADO (EMP_NOMBRE, EMP_APELLIDO, EMP_TELEFONO, EMP_DIRECCION, EMP_CORREO_ELECTRONICO)
	    values(p_nombre, p_apellido, p_telefono, p_direccion, lower(p_email));
    commit;
end;

Create or replace procedure pas_actualiza_empleado(
     p_empleado CXC_EMPLEADO.EMP_EMPLEADO%type,
     p_nombre CXC_EMPLEADO.EMP_NOMBRE%type,
     p_apellido CXC_EMPLEADO.EMP_APELLIDO%type,
     p_telefono CXC_EMPLEADO.EMP_TELEFONO%type,
     p_direccion CXC_EMPLEADO.EMP_DIRECCION%type,
     p_email CXC_EMPLEADO.EMP_CORREO_ELECTRONICO%type
)
is
        v_empleado_encontrado int;
        v_correo_duplicado int;
begin
    select 
          case when exists(select EMP_EMPLEADO from CXC_EMPLEADO where
          EMP_EMPLEADO = p_empleado and EMP_FECHA_ELIMINACION is null)
	  then 1
	  else 0
       end
          into v_empleado_encontrado
       from dual;

       if (v_empleado_encontrado = 0) then raise_application_error (-20000, 'No se ha encontrado al empleado');
       end if;

	select 
		case when exists(select EMP_EMPLEADO from CXC_EMPLEADO where lower(EMP_CORREO_ELECTRONICO)= lower(p_email) and EMP_EMPLEADO !=p_empleado and EMP_FECHA_ELIMINACION is null)
		then 1
		else 0
	end
	into v_correo_duplicado
	from dual;
	if (v_correo_duplicado = 1) then
	    raise_application_error(-20000, 'El email ya se encuentra registrado');
    end if;
    update CXC_EMPLEADO
        set 
            EMP_NOMBRE = case when p_nombre is null
                    then EMP_NOMBRE
                    else p_nombre
                end,
            EMP_APELLIDO = case when p_apellido is null
                    then EMP_APELLIDO
                    else p_apellido
                end,
            EMP_TELEFONO = case when p_telefono is null
                    then EMP_TELEFONO
                    else p_telefono
                end,
            EMP_CORREO_ELECTRONICO = case when p_email is null
                    then EMP_CORREO_ELECTRONICO
                    else lower(p_email)
                end,
           EMP_DIRECCION = case when p_direccion is null
                    then EMP_DIRECCION
                    else lower(p_direccion)
                end
            
        where EMP_EMPLEADO = p_empleado;
   
        commit;
end;

create or replace procedure pas_eliminar_empleado(
        p_empleado CXC_EMPLEADO.EMP_EMPLEADO%type
    ) 
    is
        v_empleado_encontrado int;
    begin
        select 
                case when exists(select EMP_EMPLEADO from CXC_EMPLEADO where EMP_EMPLEADO = p_empleado and EMP_FECHA_ELIMINACION is null)
                    then 1
                    else 0
                end
                into v_empleado_encontrado
            from dual;

        if (v_empleado_encontrado = 0) then
            raise_application_error(-20000, 'No se ha encontrado al empleado');
        end if;

        update CXC_EMPLEADO
        set EMP_FECHA_ELIMINACION = sysdate
        where EMP_EMPLEADO = p_empleado;
        
        commit;
end;

CREATE OR REPLACE FUNCTION fas_listar_empleado RETURN t_cxc_empleado_table AS
   cursor c_empleado is select * from CXC_EMPLEADO where EMP_FECHA_ELIMINACION is null;
    v_tabla  t_cxc_empleado_table := t_cxc_empleado_table();
    v_EMPLEADO CXC_EMPLEADO.EMP_EMPLEADO%type;
    v_NOMBRE CXC_EMPLEADO.EMP_NOMBRE%type;
    v_APELLIDO CXC_EMPLEADO.EMP_APELLIDO%type;
    v_TELEFONO CXC_EMPLEADO.EMP_TELEFONO%type;
    v_CORREO_ELECTRONICO CXC_EMPLEADO.EMP_CORREO_ELECTRONICO%type;
    v_DIRECCION CXC_EMPLEADO.EMP_DIRECCION%type;
    v_FECHA_CREACION CXC_EMPLEADO.EMP_FECHA_CREACION%type;
    v_FECHA_ELIMINACION CXC_EMPLEADO.EMP_FECHA_ELIMINACION%type;
BEGIN
    open c_empleado;
        LOOP
            fetch c_empleado into v_EMPLEADO,v_NOMBRE,v_APELLIDO,v_TELEFONO,v_CORREO_ELECTRONICO,v_DIRECCION,v_FECHA_CREACION,v_FECHA_ELIMINACION;
            exit when c_empleado %NOTFOUND;
            v_tabla.extend;
            v_tabla(v_tabla.last) := t_cxc_empleado_record(v_EMPLEADO,v_NOMBRE,v_APELLIDO,v_TELEFONO,v_CORREO_ELECTRONICO,v_DIRECCION,v_FECHA_CREACION,v_FECHA_ELIMINACION);
        END LOOP;
    close c_empleado;
  RETURN v_tabla;
END;

CREATE OR REPLACE FUNCTION fas_buscar_id_empleado (p_id number) RETURN t_cxc_empleado_table AS
   cursor c_empleado is select * from cxc_empleado where emp_fecha_eliminacion is null and EMP_EMPLEADO = p_id;
    v_tabla  t_cxc_empleado_table := t_cxc_empleado_table();
    v_EMPLEADO CXC_EMPLEADO.EMP_EMPLEADO%type;
    v_NOMBRE CXC_EMPLEADO.EMP_NOMBRE%type;
    v_APELLIDO CXC_EMPLEADO.EMP_APELLIDO%type;
    v_TELEFONO CXC_EMPLEADO.EMP_TELEFONO%type;
    v_CORREO_ELECTRONICO CXC_EMPLEADO.EMP_CORREO_ELECTRONICO%type;
    v_DIRECCION CXC_EMPLEADO.EMP_DIRECCION%type;
    v_FECHA_CREACION CXC_EMPLEADO.EMP_FECHA_CREACION%type;
    v_FECHA_ELIMINACION CXC_EMPLEADO.EMP_FECHA_ELIMINACION%type;
BEGIN
    open c_empleado;
        LOOP
            fetch c_empleado into v_EMPLEADO,v_NOMBRE,v_APELLIDO,v_TELEFONO,v_CORREO_ELECTRONICO,v_DIRECCION,v_FECHA_CREACION,v_FECHA_ELIMINACION;
            exit when c_empleado %NOTFOUND;
            v_tabla.extend;
            v_tabla(v_tabla.last) := t_cxc_empleado_record(v_EMPLEADO,v_NOMBRE,v_APELLIDO,v_TELEFONO,v_CORREO_ELECTRONICO,v_DIRECCION,v_FECHA_CREACION,v_FECHA_ELIMINACION);
        END LOOP;
    close c_empleado;
  RETURN v_tabla;
END;
