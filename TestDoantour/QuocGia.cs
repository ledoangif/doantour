using Doantour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDoantour
{
    public partial class Tests
    {
        public static IEnumerable<TestCaseData> CountryTestCasesInsert
        {
            get
            {
                yield return new TestCaseData(new Country
                {
                    CountryName = "Vietnam2",
                    ContinentName = "Asia",
                    CountryImage = "vietnam.jpg"
                });
            }
        }

        [Test]
        [TestCaseSource(nameof(CountryTestCasesInsert))]
        public async Task InsertCountryTest(Country country)
        {

            var result = await _countryService.InsertAsync(country);

            Assert.True(result.Status, result.Message);
        }

        public static IEnumerable<TestCaseData> CountryTestCasesUpdate
        {
            get
            {
                yield return new TestCaseData(1, new Country
                {
                    CountryName = "Vietnam",
                    ContinentName = "Asia",
                    CountryImage = "vietnam.jpg"
                });
                yield return new TestCaseData(2, new Country
                {
                    CountryName = "Japan",
                    ContinentName = "Asia",
                    CountryImage = "japan.jpg"
                });
            }
        }

        [Test]
        [TestCaseSource(nameof(CountryTestCasesUpdate))]
        public async Task UpdateCountryTest(int id, Country country)
        {

            var result = await _countryService.UpdateAsync(id,country);

            Assert.True(result.Status, result.Message);
        }

        [Test]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public async Task DeleteCountryTest(int id)
        {

            var result = await _countryService.DeleteAsync(id);

            Assert.True(result.Status, result.Message);
        }

        [Test]
        [TestCase("Việt Nam")]
        [TestCase("Hàn Quốc")]
        [TestCase("Singapore")]
        public async Task SearchCountryTest(string name)
        {

            var result = await _countryService.SearchAsync(name);

            Assert.True(result.Status, result.Message);
        }
    }
}
