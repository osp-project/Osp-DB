using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osp_DB
{
    public static class Test
    {
        // A function that tests all functions
        public static void TestAllFunctions()
        {
            // Create a DBStructs object to access the functions
            DBMain db = new DBMain();
            // Create some sample data to use for testing
            string json_name = "test";
            string type = "string";
            string label = "name";
            object obj = "Bing";
            SearchFilter filter = new SearchFilter(type, label, obj);
            string new_type = "text";
            string new_label = "title";
            object new_obj = "Hello";
            // Test the Init function
            Console.WriteLine("Testing Init function...");
            DBStructs.DBRet init_result = db.Init();
            // Check if the status is true and no error message is returned
            if (init_result.status && init_result.err_msg == null)
            {
                Console.WriteLine("Init function passed.");
            }
            else
            {
                Console.WriteLine("Init function failed.");
                Console.WriteLine("Error message: " + init_result.err_msg);
            }
            // Test the InitTargetJsonFile function
            Console.WriteLine("Testing InitTargetJsonFile function...");
            DBStructs.DBRet init_target_result = db.InitTargetJsonFile(json_name);
            // Check if the status is true and no error message is returned
            if (init_target_result.status && init_target_result.err_msg == null)
            {
                Console.WriteLine("InitTargetJsonFile function passed.");
            }
            else
            {
                Console.WriteLine("InitTargetJsonFile function failed.");
                Console.WriteLine("Error message: " + init_target_result.err_msg);
            }
            // Test the AddContent function
            Console.WriteLine("Testing AddContent function...");
            DBStructs.DBRet add_content_result = db.AddContent(json_name, type, label, obj);
            // Check if the status is true and no error message is returned
            if (add_content_result.status && add_content_result.err_msg == null)
            {
                Console.WriteLine("AddContent function passed.");
            }
            else
            {
                Console.WriteLine("AddContent function failed.");
                Console.WriteLine("Error message: " + add_content_result.err_msg);
            }

            // Test the SearchWithFilter function
            Console.WriteLine("Testing SearchWithFilter function...");
            List<DBStructs.DBNormalJson> search_result = db.SearchWithFilter(json_name, filter);
            // Check if the result list contains one item that matches the filter
            if (search_result.Count == 1 && filter.Matches(search_result[0]))
            {
                Console.WriteLine("SearchWithFilter function passed.");
            }
            else
            {
                Console.WriteLine("SearchWithFilter function failed.");
                Console.WriteLine("Expected one item that matches the filter, but got " + search_result.Count);
            }

            // Test the ModifyType function
            Console.WriteLine("Testing ModifyType function...");
            DBStructs.DBRet modify_type_result = db.ModifyType(json_name, filter, new_type);
            // Check if the status is true and no error message is returned
            if (modify_type_result.status && modify_type_result.err_msg == null)
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Check if the first item in the list has the new type
                if (json[0].type == new_type)
                {
                    Console.WriteLine("ModifyType function passed.");
                    // Update the filter's type to match the new type
                    filter.Type = new_type;
                }
                else
                {
                    Console.WriteLine("ModifyType function failed.");
                    Console.WriteLine("Expected type to be " + new_type + ", but got " + json[0].type);
                }
            }
            else
            {
                Console.WriteLine("ModifyType function failed.");
                Console.WriteLine("Error message: " + modify_type_result.err_msg);
            }

            // Test the ModifyLabel function
            Console.WriteLine("Testing ModifyLabel function...");
            DBStructs.DBRet modify_label_result = db.ModifyLabel(json_name, filter, new_label);
            // Check if the status is true and no error message is returned
            if (modify_label_result.status && modify_label_result.err_msg == null)
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Check if the first item in the list has the new label
                if (json[0].label == new_label)
                {
                    Console.WriteLine("ModifyLabel function passed.");
                    // Update the filter's label to match the new label
                    filter.Label = new_label;
                }
                else
                {
                    Console.WriteLine("ModifyLabel function failed.");
                    Console.WriteLine("Expected label to be " + new_label + ", but got " + json[0].label);
                }
            }
            else
            {
                Console.WriteLine("ModifyLabel function failed.");
                Console.WriteLine("Error message: " + modify_label_result.err_msg);
            }

            // Test the ModifyObject function
            Console.WriteLine("Testing ModifyObject function...");
            DBStructs.DBRet modify_object_result = db.ModifyObject(json_name, filter, new_obj);
            // Check if the status is true and no error message is returned
            if (modify_object_result.status && modify_object_result.err_msg == null)
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Check if the first item in the list has the new object
                if (json[0].obj.Equals(new_obj))
                {
                    Console.WriteLine("ModifyObject function passed.");
                    // Update the filter's object to match the new object
                    filter.Obj = new_obj;
                }
                else
                {
                    Console.WriteLine("ModifyObject function failed.");
                    Console.WriteLine("Expected object to be " + new_obj + ", but got " + json[0].obj);
                }
            }
            else
            {
                Console.WriteLine("ModifyObject function failed.");
                Console.WriteLine("Error message: " + modify_object_result.err_msg);
            }

            // Test the RemoveContent function
            Console.WriteLine("Testing RemoveContent function...");
            DBStructs.DBRet remove_content_result = db.RemoveContent(json_name, filter);
            // Check if the status is true and no error message is returned
            if (remove_content_result.status && remove_content_result.err_msg == null)
            {
                Console.WriteLine("RemoveContent function passed.");
            }
            else
            {
                Console.WriteLine("RemoveContent function failed.");
                Console.WriteLine("Error message: " + remove_content_result.err_msg);
            }
        }
    }
}
