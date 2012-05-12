#language: pt-BR

Funcionalidade: Calcular Acoplamento Eferente (Ce)
	
	Acomplamento Eferente é uma métrica que indica a quantidade
	de referências de um tipo para outros.
	
	Entram na contagem: classe base, interfaces implementadas, 
	tipos dos variáveis locais, atributos e das propriedades, 
	tipos dos parâmetros em métodos e construtores, exceptions,
	propriedades e métodos estáticos (obrigado @pedroreys),
	eventos e atributos (obrigado @IsraelAece).

	Como o objetivo é identificar a "complexidade" de um tipo,
	considero também as referências "herdadas".

	Esquema do Cenário: Calcular Acoplamento Eferente para um tipo
		Dado que tenho um <tipo>
		Quando inspeciono seu acoplamento eferente
		Então obtenho <ce>

		#
		# todos os tipos tem object, boolean, string, int32, type no mínimo
		#
		# além disso todos os tipos tem os seguintes atributos:
		#	
		#	System.Runtime.TargetedPatchingOptOutAttribute,
        #	System.Security.SecuritySafeCriticalAttribute,
        #	System.Runtime.ConstrainedExecution.ReliabilityContractAttribute,
		#
		# propriedades automáticas geram "atributos de classe" marcados com:
		#
		#	System.Runtime.CompilerServices.CompilerGeneratedAttribute
		#

		Exemplos: 
			| tipo                              | ce |
			| Samples.EmptyClass                | 8  |
			| Samples.SingleArgCtor             | 9  |
			| Samples.SingleArgVoidMethod       | 9  |
			| Samples.FeeMethod                 | 9  |
			| Samples.DateTimeArgDateTimeMethod | 9  |
			| Samples.SingleProperty            | 10 |
			| Samples.SingleField               | 9  |
			| Samples.ExceptionRaiser           | 9  |
			| Samples.SingleNonAutoProperty     | 9  |
			| Samples.SingleEvent               | 9  |
			| Samples.Attributes                | 12 |
			| Samples.StaticMember              | 10 |
			| Samples.ClassDependsOnASubClass   | 8  |



