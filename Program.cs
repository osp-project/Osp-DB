using System.IO;
using Newtonsoft.Json;

namespace Osp_DB
{
    public class DBMain
    {

        /*
        static void Main(string[] args)
        {
            Test.TestAllFunctions();
        }
        */

        // A function that initializes the ospdb_data directory if it does not exist
        public DBStructs.DBRet Init()
        {
            try
            {
                // Check if the ospdb_data directory exists
                if (!Directory.Exists("ospdb_data"))
                {
                    // If not, create it
                    Directory.CreateDirectory("ospdb_data");
                }
                // Return a DBRet object with status true to indicate success
                return new DBStructs.DBRet
                {
                    status = true
                };
            }
            catch (Exception e)
            {
                // If an exception occurs, return a DBRet object with status false and the error message
                return new DBStructs.DBRet { status = false, err_msg = e.ToString() };
            }
        }

        // A function that initializes a target json file in the ospdb_data directory if it does not exist
        public DBStructs.DBRet InitTargetJsonFile(string json_name)
        {
            try
            {
                // Check if the target json file exists
                if (!File.Exists("ospdb_data/" + json_name + ".json"))
                {
                    // If not, create it with an empty array as the content
                    File.WriteAllText("ospdb_data/" + json_name + ".json", "[]");
                }
                // Return a DBRet object with status true to indicate success
                return new DBStructs.DBRet
                {
                    status = true
                };
            }
            catch (Exception e)
            {
                // If an exception occurs, return a DBRet object with status false and the error message
                return new DBStructs.DBRet { status = false, err_msg = e.ToString() };
            }
        }

        // A function that adds content to a target json file in the ospdb_data directory
        public DBStructs.DBRet AddContent(string json_name, string type, string label, object add_object)
        {
            try
            {
                // Read the target json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Create a new DBNormalJson object with the given type, label and object as properties
                DBStructs.DBNormalJson add_part = new DBStructs.DBNormalJson
                {
                    type = type,
                    label = label,
                    obj = add_object
                };
                // Add the new object to the list
                json.Add(add_part);
                // Write the updated list back to the target json file as serialized json string
                File.WriteAllText("ospdb_data/" + json_name + ".json", JsonConvert.SerializeObject(json));
                // Return a DBRet object with status true to indicate success
                return new DBStructs.DBRet { status = true };
            }
            catch (Exception e)
            {
                // If an exception occurs, return a DBRet object with status false and the error message
                return new DBStructs.DBRet { status = false, err_msg = e.ToString() };
            }
        }

        // A function that removes content from a json file that matches a given filter
        public DBStructs.DBRet RemoveContent(string json_name, SearchFilter filter)
        {
            try
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Create a new list to store the items that do not match the filter
                List<DBStructs.DBNormalJson> new_json = new List<DBStructs.DBNormalJson>();
                // Loop through the original list and check each item against the filter
                foreach (DBStructs.DBNormalJson item in json)
                {
                    // If the item does not match the filter, add it to the new list
                    if (!filter.Matches(item))
                    {
                        new_json.Add(item);
                    }
                }
                // Write the new list back to the json file
                File.WriteAllText("ospdb_data/" + json_name + ".json", JsonConvert.SerializeObject(new_json));
                return new DBStructs.DBRet { status = true };
            }
            catch (Exception e)
            {
                return new DBStructs.DBRet { status = false, err_msg = e.ToString() };
            }
        }

        // A function that searches for content in a json file that matches a given filter and returns a list of matching items
        public List<DBStructs.DBNormalJson> SearchWithFilter(string json_name, SearchFilter filter)
        {
            // Read the json file and deserialize it into a list of DBNormalJson objects
            List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
            // Create a new list to store the items that match the filter
            List<DBStructs.DBNormalJson> result = new List<DBStructs.DBNormalJson>();
            // Loop through the original list and check each item against the filter
            foreach (DBStructs.DBNormalJson item in json)
            {
                // If the item matches the filter, add it to the result list
                if (filter.Matches(item))
                {
                    result.Add(item);
                }
            }
            // Return the result list
            return result;
        }

        // A function that modifies the type of content in a json file that matches a given filter to a new type
        public DBStructs.DBRet ModifyType(string json_name, SearchFilter filter, string new_type)
        {
            try
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Loop through the original list and check each item against the filter
                foreach (DBStructs.DBNormalJson item in json)
                {
                    // If the item matches the filter, change its type to the new type
                    if (filter.Matches(item))
                    {
                        item.type = new_type;
                    }
                }
                // Write the modified list back to the json file
                File.WriteAllText("ospdb_data/" + json_name + ".json", JsonConvert.SerializeObject(json));
                return new DBStructs.DBRet { status = true };
            }
            catch (Exception ex) { return new DBStructs.DBRet { status = false, err_msg = ex.ToString() }; }
        }

        // A function that modifies the label of content in a json file that matches a given filter to a new label
        public DBStructs.DBRet ModifyLabel(string json_name, SearchFilter filter, string new_label)
        {
            try
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Loop through the original list and check each item against the filter
                foreach (DBStructs.DBNormalJson item in json)
                {
                    // If the item matches the filter, change its label to the new label
                    if (filter.Matches(item))
                    {
                        item.label = new_label;
                    }
                }
                // Write the modified list back to the json file
                File.WriteAllText("ospdb_data/" + json_name + ".json", JsonConvert.SerializeObject(json));
                return new DBStructs.DBRet { status = true };
            }
            catch (Exception e) { return new DBStructs.DBRet { status = false, err_msg = e.ToString() }; }
        }

        // A function that modifies the object of content in a json file that matches a given filter to a new object
        public DBStructs.DBRet ModifyObject(string json_name, SearchFilter filter, object new_object)
        {
            try
            {
                // Read the json file and deserialize it into a list of DBNormalJson objects
                List<DBStructs.DBNormalJson> json = JsonConvert.DeserializeObject<List<DBStructs.DBNormalJson>>(File.ReadAllText("ospdb_data/" + json_name + ".json"));
                // Loop through the original list and check each item against the filter
                foreach (DBStructs.DBNormalJson item in json)
                {
                    // If the item matches the filter, change its object to the new object
                    if (filter.Matches(item))
                    {
                        item.obj = new_object;
                    }
                }
                // Write the modified list back to the json file
                File.WriteAllText("ospdb_data/" + json_name + ".json", JsonConvert.SerializeObject(json));
                return new DBStructs.DBRet { status = true };
            }
            catch (Exception e) { return new DBStructs.DBRet { status = false, err_msg = e.ToString() }; }
        }
    }

    public class SearchFilter
    {
        public string Type { get; set; } // The type to match, or null if any type is acceptable
        public string Label { get; set; } // The label to match, or null if any label is acceptable
        public object Obj { get; set; } // The object to match, or null if any object is acceptable

        // A constructor that takes the type, label and object as parameters
        public SearchFilter(string type, string label, object obj)
        {
            Type = type;
            Label = label;
            Obj = obj;
        }

        // A method that checks if a given DBNormalJson matches the filter criteria
        public bool Matches(DBStructs.DBNormalJson item)
        {
            // If the type is not null and does not match the item's type, return false
            if (Type != null && Type != item.type) return false;
            // If the label is not null and does not match the item's label, return false
            if (Label != null && Label != item.label) return false;
            // If the object is not null and does not match the item's object using Equals method, return false
            if (Obj != null && !Obj.Equals(item.obj)) return false;
            // Otherwise, return true
            return true;
        }
    }
}