﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.269
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace FluentCodeMetrics.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Calcular Acoplamento Aferente (Ca) incluindo assemblies na contagem")]
    public partial class CalcularAcoplamentoAferenteCaIncluindoAssembliesNaContagemFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AfferentCouplingIncludingAssemblies.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "Calcular Acoplamento Aferente (Ca) incluindo assemblies na contagem", "\r\nElvis deseja calcular o acoplamento aferente para alguns tipos,\r\nentretanto, el" +
                    "e deseja que referências de outros assemblies também sejam consideradas.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Calcular Acoplamento Aferente para um tipo")]
        [NUnit.Framework.TestCaseAttribute("Samples.Ca.Foo", "FluentCodeMetrics.Specs", "FluentCodeMetrics.Specs", "1", new string[0])]
        [NUnit.Framework.TestCaseAttribute("AnotherAssembly.Samples.Ca.ExternalFoo", "FluentCodeMetrics.Specs.AnotherAssemblySamples", "FluentCodeMetrics.Specs", "1", new string[0])]
        public virtual void CalcularAcoplamentoAferenteParaUmTipo(string tipo, string assembly, string assemblyExterno, string ca, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Calcular Acoplamento Aferente para um tipo", exampleTags);
#line 8
 this.ScenarioSetup(scenarioInfo);
#line 9
  testRunner.Given(string.Format("vou trabalhar com o {0}", assembly));
#line 10
  testRunner.And(string.Format("que tenho um {0}", tipo));
#line 11
  testRunner.When("desejo obter seu acoplamento aferente");
#line 12
  testRunner.And(string.Format("considero as referências vindas do assembly {0}", assemblyExterno));
#line 13
  testRunner.Then(string.Format("obtenho {0}", ca));
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
