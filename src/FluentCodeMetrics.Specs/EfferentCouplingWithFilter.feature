#language: pt-BR

Funcionalidade: Calcular Acoplamento Eferente (Ce) com um Filtro
	
	Elvis deseja calcular o acoplamento eferente para alguns tipos,
	entretanto, ele deseja que alguns tipos sejam ignorados na contagem.

	Esquema do Cenário: Calcular Acoplamento Eferente para um tipo
		Dado que tenho um <tipo>
		Quando desejo obter seu acoplamento eferente
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
		Então obtenho <ce>

		Exemplos: 
			| tipo                                | ce |
			| Samples.EmptyClass                  | 0  |
			| Samples.AbstractMethod              | 1  |
			| Samples.VirtualMethod               | 0  |
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
			| Samples.TryCatch                    | 1  |
			| Samples.TryCatchCustomException     | 1  |
			| Samples.TryCatchWithUndefinedType   | 0  |

	Esquema do Cenário: Calcular Acoplamento Eferente para um tipo, considerando apenas tipos do mesmo assembly
		Dado que tenho um <tipo>
		Quando desejo obter seu acoplamento eferente
		E desejo ignorar referências para tipos de outros assemblies
		Então obtenho <ce>

		Exemplos: 
			| tipo                                | ce |
			| Samples.EmptyClass                  | 0  |
			| Samples.AbstractMethod              | 1  |
			| Samples.VirtualMethod               | 0  |
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
			| Samples.TryCatch                    | 0  |
			| Samples.TryCatchCustomException     | 1  |
			| Samples.TryCatchWithUndefinedType   | 0  |

	Esquema do Cenário: Calcular Acoplamento Eferente para todos os tipos do assembly, considerando apenas tipos do mesmo
		Dado que desejo obter o acoplamento eferente de todos os tipos deste assembly
		E desejo ignorar referências para tipos de outros assemblies
		Então Verifico o Ce de <tipo> 
		E constato que é <ce>

		Exemplos: 
			| tipo                                | ce |
			| Samples.EmptyClass                  | 0  |
			| Samples.AbstractMethod              | 1  |
			| Samples.VirtualMethod               | 0  |
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
			| Samples.TryCatch                    | 0  |
			| Samples.TryCatchCustomException     | 1  |
			| Samples.TryCatchWithUndefinedType   | 0  |

	Esquema do Cenário: Calcular o Acoplamento Eferente para todos os tipos do assembly, considerando apenas tipos externos ao assembly
		Dado que desejo obter o acoplamento eferente de todos os tipos deste assembly
		E desejo ignorar referências para tipos deste assembly
		Então Verifico o Ce de <tipo>
		E constato que é <ce>

		# Todo tipo tem no mínimo 8 dependêcias
		# Maiores detalhes no arquivo EfferentCoupling.feature
		Exemplos:
			| tipo                                | ce |
			| Samples.EmptyClass                  | 8  |
			| Samples.AbstractMethod              | 8  |
			| Samples.VirtualMethod               | 8  |
			| Samples.SingleArgCtor               | 8  |
			| Samples.SingleArgVoidMethod         | 8  |
			| Samples.FeeMethod                   | 8  |
			| Samples.DateTimeArgDateTimeMethod   | 9  |
			| Samples.SingleProperty              | 10 |
			| Samples.SingleField                 | 9  |
			| Samples.ExceptionRaiser             | 9  |
			| Samples.SingleNonAutoProperty       | 9  |
			| Samples.SingleEvent                 | 9  |
			| Samples.Attributes                  | 10 |
			| Samples.StaticMember                | 10 |
			| Samples.ClassDependsOnASubClass     | 8  |
			| Samples.StaticPropertyAndMethodCall | 10 |
			| Samples.TryCatch                    | 9  |
			| Samples.TryCatchCustomException     | 8  |
			| Samples.TryCatchWithUndefinedType   | 8  |





