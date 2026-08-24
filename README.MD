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


### Project Flow & Architecture  

