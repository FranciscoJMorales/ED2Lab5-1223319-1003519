# ED2Lab5-1223319-1003519

Formato de las llaves para los cifrados:

Cifrado César: texto
- Cualquier texto que contenga únicamente letras del abecedario (excepto ñ). Puede tener mayúsculas y minúsculas.
Ejemplo: Estructuras

Cifrado ZigZag: n
- n = Número de filas (mayor a 0)
Ejemplo: 5

Cifrado de Ruta: tipo:nXm
- tipo: Recorrido para el método (vertical o espiral)
- n = Número de filas (mayor a 0)
- m = Número de columnas (mayor a 0)
Ejemplo 1: vertical:4X5
Ejemplo 2: espiral:7X7

Si las llaves no cuentan con el formato correcto, el api devuelve InternalServerError con un mensaje indicando el formato correcto de las llaves.
