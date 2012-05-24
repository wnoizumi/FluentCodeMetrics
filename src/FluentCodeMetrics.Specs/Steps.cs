using System;
using System.Linq;
using FluentCodeMetrics.Core;

using SharpTestsEx;
using TechTalk.SpecFlow;
using System.Reflection;


namespace FluentCodeMetrics.Specs
{
    [Binding]
    public class Steps
    {
        private CodeMetric resultingMetric;
        private Type workingType;
        
        [Given(@"que tenho um (.*)")]
        public void DadoQueTenhoUm(string tipo)
        {
            workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();


        }

        private MethodInfo workingMethod;
        [Given(@"nesse tipo há um método chamado (.*)")]
        public void DadoNesseTipoHaUmMetodoChamado(string nomeDoMetodo)
        {
            workingMethod = workingType.GetMethod(nomeDoMetodo);
        }

        [When(@"desejo obter sua Complexidade Ciclomática \(Cc\)")]
        public void QuandoDesejoObterSuaComplexidadeCiclomaticaCc()
        {
            resultingMetric = Cc.For(workingMethod);
        }

        [When(@"desejo obter seu acoplamento eferente")]
        public void QuandoDesejoObterSeuAcoplamentoEferente()
        {
            resultingMetric = Ce.For(workingType);
        }

        
        [When(@"tenho um fitro de referências que desejo ignorar")]
        public void QuandoTenhoUmFitroDeReferenciasQueDesejoIgnorar()
        {
        }

        [When(@"desejo ignorar referências para tipos de outros assemblies")]
        [Given(@"desejo ignorar referências para tipos de outros assemblies")]
        public void DesejoIgnorarReferenciasParaTiposDeOutrosAssemblies()
        {
            resultingMetric = ((Ce)resultingMetric).Ignoring(GetType().Assembly.Not());
        }


        [When(@"esse filtro relaciona (.*)")]
        public void QuandoEsseFiltroRelaciona(string tipo)
        {
            var type = Type.GetType(tipo);
            type.Should().Not.Be.Null();

            resultingMetric = ((Ce)resultingMetric).Ignoring(type);
        }

        [When(@"inspeciono seu acoplamento eferente considerando esse filtro")]
        public void QuandoInspecionoSeuAcoplamentoEferenteConsiderandoEsseFiltro()
        {
        }

        [When(@"inspeciono seu acoplamento eferente")]
        public void QuandoInspecionoSeuAcoplamentoEferente()
        {
        }

        [Then(@"obtenho (.*)")]
        public void EntaoObtenho(int value)
        {
            resultingMetric.Value.Should().Be(value);
        }

        [Given(@"que desejo obter o acoplamento eferente de todos os tipos deste assembly")]
        public void DadoQueDesejoObterOAcoplamentoEferenteDeTodosOsTiposDesseAssembly()
        {
            resultingMetric = Ce.For(GetType().Assembly);
        }

        [Then(@"Verifico o Ce de (.*)")]
        public void EntaoVerificoOCeDe(string tipo)
        {
            workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();
        }

        [Then(@"constato que é (.*)")]
        public void EntaoConstatoQueE(int valor)
        {
            var typeCe = ((TypeSetCe) resultingMetric).Source
                .Where(ce => ce.GetType() == typeof(TypeCe))
                .Cast<TypeCe>()
                .First(ce => ce.Type == workingType);

            typeCe.Value.Should().Be(valor);
        }

    }
}