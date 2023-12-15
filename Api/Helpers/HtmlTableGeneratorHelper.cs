using System.Reflection;

namespace Api.Helpers;

public static class HtmlTableGeneratorHelper
{
    public static string GenerateHtmlTable<T>(T dto)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        string tableHtml = @"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    table {
                        font-family: Arial, sans-serif;
                        border-collapse: collapse;
                        width: 100%;
                    }
                    th, td {
                        border: 1px solid #dddddd;
                        text-align: left;
                        padding: 8px;
                    }
                    th {
                        background-color: #f2f2f2;
                    }
                </style>
            </head>
            <body>
                <table>
                    <tr>
                        <th>Property</th>
                        <th>Value</th>
                    </tr>";

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(dto);
            tableHtml += $@"
                    <tr>
                        <td>{property.Name}</td>
                        <td>{(value != null ? value.ToString() : "null")}</td>
                    </tr>";
        }

        tableHtml += @"
                </table>
            </body>
            </html>";

        return tableHtml;
    }
}