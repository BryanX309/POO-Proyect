# Proyecto Final POO

**Gestión de Contadores y Facturas del Servicio Eléctrico**

## Tablas a Utilizar

1. Contadores Electricos (meters)
2. Facturas Emitidas (bills)

## Campos Comunes Tablas

- Id
- Creado por id
- Fecha de Creación
- modificado por id
- Fecha de Modification

## Campos Tabla meters

- supplyKey (Clave de Suministro)
- clientId (Id Cliente)
- consumptionType (Tipo de Consumo)
- rate (Tarifa)
- comercialSector (Sector Comercial)
- isActive (esta Activo)

## Campos Tabla bills

- meterId (Information del Medidor)
- dueDate (Fecha de Vencimiento)
- totalAmountDue (Total a Pagar)
- previousReading (Lectura Anterior)
- currentReading (Lectura Actual)
- previousReadingDate (Fecha Lectura anterior)
- currentReadingDate (Fecha Lectura Actual)