## Iron Grid C2

***-Note! all of those command are tested on linux, there might be a small differences on windows or mac.***

***-Important note! this all project works with docker compose, You don't need to run the servers by hand.***

----

#### How To Run 


**1. Make sure you have `Docker` And `git` installed on your computer**

*2. Run `git clone https://github.com/ShlomoShoov/IronGridC2.git`*

*3. Run `cd IronGridC2`*

*4. Run `mv .env.example .env`*

***Note! the file .env.example is just an example... (it should work anyway but contain very week passwords) make sure to replace passwords, you cam also can replace the API outside port, so you can test the project on any port on your local host***

*5. Run `docker compose up --build -d`*

***Note! if this command fails check if you have containers with the same name as in the file docker-compose.yaml, or other containers with prot that in use in docker-compose.yaml***

***Note! this command will take some time, (downloads images, build and download microsoft packages and restart servers)***

--- 

### Use the API:

Go to http://localhost:8000/swagger for visual api testing (If you change the port on the .env file, make sure to replace it here!)

---

### Project Flow & Architecture  

When You Run `docker compose up` 6 servers are started by that order:

Kafka -> Producer -> Mysql -> Consumer -> Redis -> API

Note that every server needs that all of the containers before to started

**Producer Flow**

    - Loading the raw data from Data/field_report.json and converting to `Report Objects` 
    - For each Report, Produce it to specific kafka topic (by its AssetType)
    - When there is no more report that's loaded from the file, we flush and dispose.

**Producer Architecture**
    
    - I use interface of ILoader which return Enumerator, so we can replace it easily with API, or multiple files
    - I send the topic names from the .env file, to make sure they will be all the same in all the systems
    - I Created a Generic logger so I it will be easy to follow the system flow


**Consumer Flow**

    - We init the system, Loading the seed sql file and send it to the sql as a raw command, so all of the tables and the base data will create
    - In endless loop we listen to new messages from kafka to the topics we need
    - for each report that comes we send it to the processor 
    - the processor convert it into the AssetLiveReport and than calculate the logic business and return a object to send to the data base
    - than we send that live status to the repository, which checks if we already have a status for the asset, if yes, update it, otherwise, created a new one.


### Notes

    - In the PUT `/api/assets/{id}` I assume That we Should allow changing the Unit Id field, so I added check to make sure the new unit exists, if not , return 404 (as required), but I should return 422 or 400, but I do as required.
    - In the POST `/api/assets/units` the Id assign automatically by the Db, so we should return the new Id, but I did as required.
    