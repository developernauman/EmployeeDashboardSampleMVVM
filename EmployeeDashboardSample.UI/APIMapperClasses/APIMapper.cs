using EmployeeDashboardSample.UI.APIModelsMapper;
using EmployeeDashboardSample.UI.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeDashboardSample.UI
{
    public static class APIMapper
    {
        static string baseUrl = "https://localhost:44392/";

        static async Task<T> CallGetAPIs<T>(string apiPath)
        {
            T returnobject = default;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage response = await client.GetAsync(apiPath);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<Response<T>>();
                        if (result.Success && result.Payload != null)
                            returnobject = result.Payload;
                        else if (result.Error != null && !string.IsNullOrEmpty(result.Error.Message))
                            MessageBox.Show(result.Error.Message);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return returnobject;
        }

        static async Task<bool> CallDeleteAPIs(string apiPath)
        {
            bool returnobject = false;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage response = await client.DeleteAsync(apiPath);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                        if (result.Success)
                            returnobject = result.Payload;
                        else if (result.Error != null && !string.IsNullOrEmpty(result.Error.Message))
                            MessageBox.Show(result.Error.Message);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return returnobject;
        }

        static async Task<T> CallPutAPIs<T>(string apiPath, T value)
        {
            T returnobject = default;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage response = await client.PutAsJsonAsync<T>(apiPath, value);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<Response<T>>();
                        if (result.Success && result.Payload != null)
                            returnobject = result.Payload;
                        else if (result.Error != null && !string.IsNullOrEmpty(result.Error.Message))
                            MessageBox.Show(result.Error.Message);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return returnobject;
        }



        public static async Task<bool> DeleteEmployeeById(long employeeId)
        {
            return await CallDeleteAPIs(string.Format("{0}/{1}", "SampleApi/DeleteEmployeeById", employeeId));
        }

        public static async Task<IEnumerable<EmployeeDetails>> GetEmployeesDetails()
        {
            return await CallGetAPIs<IEnumerable<EmployeeDetails>>("SampleApi/GetEmployeesDetails");
        }

        public static async Task<EmployeeDetails> GetEmployeeDetailsById(long employeeId)
        {
            return await CallGetAPIs<EmployeeDetails>(string.Format("{0}/{1}", "SampleApi/GetEmployeeDetailsById", employeeId));
        }

        public static async Task<EmployeeDetails> AddOrUpdateEmployeeDetails(EmployeeDetails employeeDetails)
        {
            return await CallPutAPIs<EmployeeDetails>("SampleApi/AddOrUpdateEmployee", employeeDetails);
        }
    }
}
