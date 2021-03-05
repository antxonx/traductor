# Seminario de Traductores de lenguaje II

Proyecto realizado en C# con la configuración de netcoreapp3.1 en Visual Studio 2019

## 1. Analizador Léxico

Se encuentra en la carpeta del proyecto `/lexico`

Analizador léxico que identifica correctamente los tipos **Entero**, **Real** e **Identificador**. Devuelve **Indefinido** en caso de no coincidir con estos tipos.

El proceso de reconocimiento se describe en el siguiente automata:

![Automata de analizador léxico](/lexico/res/auto-min.png)

Donde:

* `Q0` es el estado inicial.
* `Q1` es el estado final para `enteros`
* `Q3` es el estado final para `reales`
* `Q4` es el estado final para `identificadores`
* Cualquier otro caso queda como `indefinido`

El archivo `entry.txt` define los parametros de entrada a ser analizados.

El resultado esperado:

![Resultado de ejecutar el analizador léxico](/lexico/res/lexico-min.png)

## 2. Analizador Léxico completo

Se encuentra en la carpeta del proyecto `/CompleteLex`

Identifica correctamente todos los siguinetes símbolos:

![Resultado de la ejecución del analizador léxico](/CompleteLex/res/lexico-min.png)