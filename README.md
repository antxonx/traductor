# Seminario de Traductores de lenguaje II

Proyecto realizado en C# con la configuraci�n de netcoreapp3.1 en Visual Studio 2019

## 1. Analizador L�xico

Se encuentra en la carpeta del proyecto `/lexico`

Analizador l�xico que identifica correctamente los tipos **Entero**, **Real** e **Identificador**. Devuelve **Indefinido** en caso de no coincidir con estos tipos.

El proceso de reconocimiento se describe en el siguiente automata:

![Automata de analizador l�xico](/lexico/res/auto-min.png)

Donde:

* `Q0` es el estado inicial.
* `Q1` es el estado final para `enteros`
* `Q3` es el estado final para `reales`
* `Q4` es el estado final para `identificadores`
* Cualquier otro caso queda como `indefinido`

El archivo `entry.txt` define los parametros de entrada a ser analizados.

El resultado esperado:

![Resultado de ejecutar el analizador l�xico](/lexico/res/lexico-min.png)

## 2. Analizador L�xico completo

Se encuentra en la carpeta del proyecto `/CompleteLex`

Identifica correctamente todos los siguinetes s�mbolos:

![Resultado de la ejecuci�n del analizador l�xico](/CompleteLex/res/lexico-min.png)