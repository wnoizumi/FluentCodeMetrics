using System;
using System.Linq;
using FluentCodeMetrics.Core;

using SharpTestsEx;
using TechTalk.SpecFlow;


namespace FluentCodeMetrics.Specs
{
    [Binding]
    public class EfferentCouplingSteps
    {
        private Ce resultingCe;
        
        [Given(@"que tenho um (.*)")]
        public void DadoQueTenhoUm(string tipo)
        {
            var workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();

            resultingCe = Ce.For(workingType);
        }

        [When(@"desejo obter seu acoplamento eferente")]
        public void QuandoDesejoObterSeuAcoplamentoEferente()
        {
            //ScenarioContext.Current.Pending();
        }

        
        [When(@"tenho um fitro de referências que desejo ignorar")]
        public void QuandoTenhoUmFitroDeReferenciasQueDesejoIgnorar()
        {
        }

        [When(@"desejo ignorar referências para tipos de outros assemblies")]
        [Given(@"desejo ignorar referências para tipos de outros assemblies")]
        public void DesejoIgnorarReferenciasParaTiposDeOutrosAssemblies()
        {
            resultingCe = resultingCe.Ignoring(GetType().Assembly.Not());
        }


        [When(@"esse filtro relaciona (.*)")]
        public void QuandoEsseFiltroRelaciona(string tipo)
        {
            var type = Type.GetType(tipo);
            type.Should().Not.Be.Null();

            resultingCe = resultingCe.Ignoring(type);
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
        public void EntaoObtenho(int ce)
        {
            resultingCe.Value.Should().Be(ce);
        }

        [Given(@"que desejo obter o acoplamento eferente de todos os tipos deste assembly")]
        public void DadoQueDesejoObterOAcoplamentoEferenteDeTodosOsTiposDesseAssembly()
        {
            resultingCe = Ce.For(GetType().Assembly);
        }


        private Type workingType;
        [Then(@"Verifico o Ce de (.*)")]
        public void EntaoVerificoOCeDe(string tipo)
        {
            workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();
        }

        [Then(@"constato que é (.*)")]
        public void EntaoConstatoQueE(int valor)
        {
            var typeCe = ((TypeSetCe) resultingCe).Source
                .Where(ce => ce.GetType() == typeof(TypeCe))
                .Cast<TypeCe>()
                .First(ce => ce.Type == workingType);

            typeCe.Value.Should().Be(valor);
        }

    }
}