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
        private int resultingCe;
        private Type workingType;
        private TypeFilter filter;

        [Given(@"que tenho um (.*)")]
        public void DadoQueTenhoUm(string tipo)
        {
            workingType = Type.GetType(tipo);
            workingType.Should().Not.Be.Null();
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

            filter = filter == null
                         ? TypeFilter.EqualsTo(type)
                         : filter.Or(type);
        }

        [When(@"inspeciono seu acoplamento eferente considerando esse filtro")]
        public void QuandoInspecionoSeuAcoplamentoEferenteConsiderandoEsseFiltro()
        {
            resultingCe = workingType.ComputeCe(filter.Not());
        }

        [When(@"inspeciono seu acoplamento eferente")]
        public void QuandoInspecionoSeuAcoplamentoEferente()
        {
            resultingCe = workingType.ComputeCe();
        }

        [Then(@"obtenho (.*)")]
        public void EntaoObtenho(int ce)
        {
            resultingCe.Should().Be(ce);
        }
    }
}