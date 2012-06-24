#language: pt-BR

Funcionalidade: Calcular Acoplamento Aferente (Ca) incluindo assemblies na contagem
	
	Elvis deseja calcular o acoplamento aferente para alguns tipos,
	entretanto, ele deseja que referências de outros assemblies também sejam consideradas.

	Esquema do Cenário: Calcular Acoplamento Aferente para um tipo
		Dado vou trabalhar com o <assembly>
		E que tenho um <tipo>
		Quando desejo obter seu acoplamento aferente 
		E considero as referências vindas do assembly <assemblyExterno>
		Então obtenho <ca>

		Exemplos: 
			| tipo                                       | assembly										  | assemblyExterno			| ca |
			| Samples.Ca.Foo						     | FluentCodeMetrics.Specs						  | FluentCodeMetrics.Specs	| 1  |
			| AnotherAssembly.Samples.Ca.ExternalFoo     | FluentCodeMetrics.Specs.AnotherAssemblySamples | FluentCodeMetrics.Specs	| 1  |