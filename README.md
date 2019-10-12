# Migrating an Existing Workload to an AWS Control Tower governed Account using CloudEndure

Scripts in this repository can be used to create an on-premises simulated environment suitable for testing and exercising
a migration using CloudEndure live migration agents of a two layer workload to a new AWS Account governed by AWS Control Tower.

The installation process creates the dotnet core application and a database, loads the data, and sets the environment up for migration using CloudEndure.

**Note:**
* Allow 15 minutes for installation of the SQL Server database sample.
* The scripts create the database: dms_sample and the login and user: dms_user/dms_user 
* Objects are created in the schema: dbo

**Resources Created by CloudFormation**
* An Amazon EC2 Ubuntu instance (t2.micro) to host the dotnet core application
* An Amazon EC2 Windows instance (t3.xlarge) to host the SQL Server instance
* The SQL Server database needs 50GB of Amazon EBS disk size
* We use the default VPC template that launches a 2 AZ model with NAT Gateway for each AZ.

##Installation Instructions
* Download the cloudformation template
* Fo to AWS CloudFormation console and launch a new stack 
...it takes 5 minutes to finish the installation and 10 minutes to finish database creation

## Repo Structure
There are five directories below this main directory. they are:
* **AspNetCoreDmsSample:** the sample dotnet core application
* **cloudformation:** cloudformation template to launch the simulated on-premises environment
* **sqlserverdb:** scripts for creating the SQL Server database - this script uses the repo XXXX, but to expedite the process doesn't create the event's tickets and It enables by default Sql Server Authentication mode.

Neither the sample application or the dms_sample database are meant as an example of how one might ideally build a ssystem,
it's designed to allow the user to get a feel for how to migrate Linux and Windows virtual machines using CloudEndure Live Migration Agent.

## Diagram of the System
![alt tag](/images/sampledb.jpg)

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

