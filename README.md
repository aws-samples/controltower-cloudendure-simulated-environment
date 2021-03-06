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

**Installation Instructions**
* Access the blog post <b><a href="https://aws.amazon.com/blogs/mt/how-to-take-advantage-of-aws-control-tower-and-cloudendure-to-migrate-workloads-to-aws/">How to take advantage of AWS Control Tower and CloudEndure to migrate workloads to AWS</a></b> to have detailed guidance about it.
...it takes 5 minutes to finish the installation and 10 minutes to finish database creation

**Clean up**
* First Step: Go to CloudEndure user console and delete the project.
* Second Step: Delete the target vpc AWS Cloudformation stack
* Third Step: Delete the on-premises AWS Cloudformation stack


## Repo Structure
There are thre directories below this main directory. they are:
* **AspNetCoreDmsSample:** the sample dotnet core application to help you to interact with the SQL Server database.
* **cloudformation:** a cloudformation template "lab-onpremises-appdb.json" to launch the simulated on-premises environment and the target environment lab-targetvpc.json. Both templates uses the <a href="https://aws.amazon.com/quickstart/architecture/vpc/">AWS Quick Start Modular and Scalable VPC Architecture</a>. These templates create a 2-AZ VPC with public and private subnets layers and one NatGateway in each VPC.
* **sqlserverdb:** scripts for creating the SQL Server database - this script uses the repo <a href="https://github.com/aws-samples/aws-database-migration-samples/tree/master/sqlserver/sampledb/v1"><b>aws-database-migration-samples</b></a>, but to expedite the process doesn't create the events' tickets and It enables by default Sql Server Authentication mode.

Neither the sample application or the dms_sample database are meant as an example of how one might ideally build an application,
it's designed to allow the user to get a feel for how to migrate Linux and Windows virtual machines using CloudEndure Live Migration Agents and getting advantage of the governed AWS Accounts created by AWS Control Tower.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

