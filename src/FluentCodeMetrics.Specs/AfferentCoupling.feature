#language: pt-BR

Funcionalidade: Calcular Acoplamento Aferente (Ca)
	
	Acomplamento Aferente é uma métrica que indica a quantidade
	de tipos que referenciam um determinado tipo.

	Esquema do Cenário: Calcular Acoplamento Aferente para um tipo
		Dado que tenho um <tipo>
		Quando desejo obter seu acoplamento aferente
		Então obtenho <ca>

		Exemplos: 
			| tipo                                       | ca |
			| Samples.Ca.Foo                             | 1  |
			| Samples.Ca.FooCalculator                   | 0  |
			| Samples.Ca.BarAttribute                    | 1  |
			| Samples.Ca.FooException                    | 2  |
			| Samples.Ca.ClassWhichReferencesExternalFoo | 0  |			