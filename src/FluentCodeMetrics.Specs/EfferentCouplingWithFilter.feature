#language: pt-BR

Funcionalidade: Calcular Acoplamento Eferente (Ce) com um Filtro
	
	Elvis deseja calcular o acoplamento eferente para alguns tipos,
	entretanto, ele deseja que alguns tipos sejam ignorados na contagem.

	Esquema do Cenário: Calcular Acoplamento Eferente para um tipo
		Dado que tenho um <tipo>
		E tenho um fitro de referências que desejo ignorar
		E esse filtro relaciona System.Runtime.TargetedPatchingOptOutAttribute
		E esse filtro relaciona System.Security.SecuritySafeCriticalAttribute
		E esse filtro relaciona System.Runtime.ConstrainedExecution.ReliabilityContractAttribute
		E esse filtro relaciona System.Runtime.CompilerServices.CompilerGeneratedAttribute
		E esse filtro relaciona System.Object
		E esse filtro relaciona System.Int32
		E esse filtro relaciona System.String
		E esse filtro relaciona System.Boolean
		E esse filtro relaciona System.Type
		Quando inspeciono seu acoplamento eferente considerando esse filtro
		Então obtenho <ce>

		Exemplos: 
			| tipo                                | ce |
			| Samples.EmptyClass                  | 0  |
			| Samples.SingleArgCtor               | 1  |
			| Samples.SingleArgVoidMethod         | 1  |
			| Samples.FeeMethod                   | 1  |
			| Samples.DateTimeArgDateTimeMethod   | 1  |
			| Samples.SingleProperty              | 1  |
			| Samples.SingleField                 | 1  |
			| Samples.ExceptionRaiser             | 1  |
			| Samples.SingleNonAutoProperty       | 1  |
			| Samples.SingleEvent                 | 1  |
			| Samples.Attributes                  | 4  |
			| Samples.StaticMember                | 1  |
			| Samples.ClassDependsOnASubClass     | 0  |
			| Samples.StaticPropertyAndMethodCall | 2  |

	Esquema do Cenário: Calcular Acoplamento Eferente para um tipo, considerando apenas tipos do mesmo assembly
		Dado que tenho um <tipo>
		Quando desejo obter seu acoplamento eferente
		E desejo ignorar referências para tipos de outros assemblies
		Então obtenho <ce>

		Exemplos: 
			| tipo                                | ce |
			| Samples.EmptyClass                  | 0  |
			| Samples.SingleArgCtor               | 1  |
			| Samples.SingleArgVoidMethod         | 1  |
			| Samples.FeeMethod                   | 1  |
			| Samples.DateTimeArgDateTimeMethod   | 0  |
			| Samples.SingleProperty              | 0  |
			| Samples.SingleField                 | 0  |
			| Samples.ExceptionRaiser             | 0  |
			| Samples.SingleNonAutoProperty       | 0  |
			| Samples.SingleEvent                 | 0  |
			| Samples.Attributes                  | 2  |
			| Samples.StaticMember                | 0  |
			| Samples.ClassDependsOnASubClass     | 0  |
			| Samples.StaticPropertyAndMethodCall | 0  |






