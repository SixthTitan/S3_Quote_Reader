/**
     * Quote Client
     * This client stand alone package will utlize the power of AWS 
     * It will retrieve a quote file from S3 and read it out to the console.
     * It does not require any server to run.
     *
     * @author Lauren Brown
     * @version 2019.1.1
     * @license See the included LICENSE.txt file for more information.
     */


using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;

namespace QuoteClient
{
    public class Quote_Client
    {
        private const string Bucket = "cs-250"; // What is the name of our bucket?
        private const string Object = "Quote_File.txt"; // What is the name of our file?

        /// <summary>
        /// Constructs a new S3 Client Service
        /// </summary>
        /// <param name="accesskey"> Our AWS Access Key</param>
        /// <param name="secret"> Our AWS Secret Key </param>
        /// <returns></returns>
        private static AmazonS3Client Config(string accesskey, string secret)
        {
            var client = new AmazonS3Client(accesskey, secret, Amazon.RegionEndpoint.USEast1);

            return client;
        }

       
        /// <summary>
        /// Constructs a new Object Request to retrieve a file from S3
        /// </summary>
        private static GetObjectRequest request = new GetObjectRequest
        {
            BucketName = Bucket,
            Key = Object
        };

        /// <summary>
        /// Uses the Config constructor to create a S3 Service 
        /// and makes a file request to an object on S3
        /// </summary>
        public static void FileRequest()
        {
            /* Create a new S3 Service Client that we can use*/
           var s3client = Config("AKIAJUUKAS6ROWG53PNQ", "Gk0VRGBk2loBI2uCZCL+XXKklvmOar1KwrGdGTnD");

            try
            {

                using (GetObjectResponse response = s3client.GetObject(request))
                {
                    using (StreamReader reader = new StreamReader(response.ResponseStream))
                    {
                        string data = reader.ReadToEnd();

                        Console.Title = "Quote Server";
                        Console.WriteLine("Message of the Day: \n");
                        Console.WriteLine(data);
                        Console.ReadLine();

                    }
                }

            } catch (AmazonS3Exception error)
            {
                /* Write to the console what has occured */
                Console.WriteLine(error.ErrorCode, "\n" +
                                  error.ErrorType, "\"," +
                                  error.Message); 
            }
              
        } 
        
        static void Main(string[] args)
        {
            FileRequest(); //Start a new S3 File Read Request
        }

    }

}

