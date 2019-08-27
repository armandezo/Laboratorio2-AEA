create procedure usp_Lista_anios
as
begin
select distinct year([FechaPedido])as anios from Pedidos;
end;

create procedure usp_Lista_Pedidos_Anios
@anios int
as
begin 
Select IdPedido,NombreCompañia,Apellidos + ' ' + Nombre as Empleado, FechaPedido,FechaEntrega 
from clientes 
inner join Pedidos on clientes.idCliente = Pedidos.IdCliente
inner join Empleados on Empleados.IdEmpleado = Pedidos.IdEmpleado
where year (FechaPedido) = @anios;
end;


create procedure usp_Detalle_pedido
@idPedido int
as
begin 
Select detallesdepedidos.idproducto,nombreProducto,detallesdepedidos.preciounidad,cantidad,detallesdepedidos.preciounidad*cantidad as Monto
from detallesdepedidos
inner join productos on detallesdepedidos.idproducto = productos.idproducto
where idpedido = @idPedido;
end;


Create procedure usp_listar_pedidos_nombre_apellido
@nombreApellido VARCHAR(40)
as
select IdPedido,IdCliente,FechaPedido,FechaEntrega,Empleados.Nombre,Empleados.Apellidos
from Pedidos
inner join Empleados on Empleados.IdEmpleado = Pedidos.IdEmpleado
where Empleados.Nombre + Empleados.Apellidos like '%'+@nombreApellido+'%'


create procedure usp_listar_pedidos
as
select IdPedido,IdCliente,FechaEntrega,FechaEnvio,Destinatario from  pedidos 


create procedure usp_nombre_meses @anio int
as
select distinct MONTH(FechaPedido) as 'idMeses', DateName(month,FechaPedido) as meses from Pedidos
where YEAR(FechaPedido) = @anio;


create procedure usp_filtrar_pedidos_año_mes
@anio varchar(4),
@mes varchar(2)
as
select IdPedido,FechaPedido,FechaEntrega,DATEDIFF(year,FechaEntrega,GETDATE()) as 'Transcurridos',Empleados.Nombre from Pedidos
inner join Empleados on Empleados.IdEmpleado = Pedidos.IdEmpleado
where FechaPedido BETWEEN @anio+'-'+@mes+'-01' AND  @anio+'-'+@mes+'-31'


