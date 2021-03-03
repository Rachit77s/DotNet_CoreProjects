Simple WebHook that accepts Order details sent to us by a payment provider and we wrote a JSON message onto the Storage Queue whenever that function was called.
Function1.cs is the starting function.

We are going to listen to a message containing details of the incoming orders and create License files which we going to place into a Blob storage container.
When the license file is created in the blob storage we will trigger a new Azure function that will email the new license file to the user.
