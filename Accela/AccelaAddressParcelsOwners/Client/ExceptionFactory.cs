/* 
 * Addresses, Parcels, Owners
 *
 * Use the Address-Parcel-Owner (\"APO\") API to get, create, and update reference information about addresses, parcels, and owners used in land or property management solutions. Because reference APO can be associated to multiple transactional records, a reference APO object cannot be deleted.
 *
 * OpenAPI spec version: v4
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */


using System;
using RestSharp;

namespace AccelaAddressParcelsOwners.Client
{
    /// <summary>
    /// A delegate to ExceptionFactory method
    /// </summary>
    /// <param name="methodName">Method name</param>
    /// <param name="response">Response</param>
    /// <returns>Exceptions</returns>
    public delegate Exception ExceptionFactory(string methodName, IRestResponse response);
}