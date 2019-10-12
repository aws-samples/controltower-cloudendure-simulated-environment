# Migrating an Existing Workload to an AWS Control Tower governed Account using CloudEndure

Scripts in this repository can be used to create an on-premises simulated environment suitable for testing and exercising
a migration using CloudEndure live migration agents of a two layer workload to a new AWS Account governed by AWS Control Tower.

The installation process creates the dotnet core application and a database, loads the data, and sets the environment up for migration using CloudEndure.

**Note:**
* Allow 15 minutes for installation of the SQL Server database sample.
* The scripts create the database: dms_sample and the login and user: dms_user/dms_user 
* Objects are created in the schema: dbo
* Don't allow 0.0.0.0/0 to access the web application, use the least access priviledge possible. We recommend your current IP.

**Resources Created by CloudFormation**
* An Amazon EC2 Ubuntu instance (t2.micro) to host the dotnet core application
* An Amazon EC2 Windows instance (t3.xlarge) to host the SQL Server instance
* The SQL Server database needs 50GB of Amazon EBS disk size
* We use the default VPC template that launches a 2 AZ model with NAT Gateways for each AZ.
* IAM role that give access to AWS Systems Manager to help you installing CloudEndure agents.

##Installation Instructions
* Download the <a href="https://github.com/aws-samples/controltower-cloudendure-simulated-environment/blob/master/cloudformation/lab-onpremises-appdb.json">cloudformation template</a>.
* Go to AWS CloudFormation console and launch a new stack using this template. Access the blog post <b>Migrating an Existing Workload to an AWS Control Tower governed Account using CloudEndure</b> to have detailed guidance about it.
...it takes 5 minutes to finish the installation and 10 minutes to finish database creation

## Repo Structure
There are five directories below this main directory. they are:
* **AspNetCoreDmsSample:** the sample dotnet core application to help you to interact with the SQL Server database.
* **cloudformation:** cloudformation template to launch the simulated on-premises environment
* **sqlserverdb:** scripts for creating the SQL Server database - this script uses the repo XXXX, but to expedite the process doesn't create the event's tickets and It enables by default Sql Server Authentication mode.

Neither the sample application or the dms_sample database are meant as an example of how one might ideally build a ssystem,
it's designed to allow the user to get a feel for how to migrate Linux and Windows virtual machines using CloudEndure Live Migration Agent.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

