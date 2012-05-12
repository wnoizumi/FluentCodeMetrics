using System;
using FluentCodeMetrics.Core;
using SharpTestsEx;
using TechTalk.SpecFlow;
using FluentCodeMetrics.Core.TypeFilters;

namespace FluentCodeMetrics.Specs
{
    [Binding]
    public class EfferentCouplingSteps
    {
        private Ce resultingCe;
        private TypeFilter filter;

        [Given(@"que tenho um (.*)")]
        public void DadoQueTenhoUm(string tipo)
        {
            var workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();

            resultingCe = Ce.For(workingType);
        }

        
        [Given(@"tenho um fitro de referências que desejo ignorar")]
        public void DadoTenhoUmFitroDeReferenciasQueDesejoIgnorar()
        {
        }

        [Given(@"esse filtro relaciona (.*)")]
        public void DadoEsseFiltroRelaciona(string tipo)
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
    }
}