## Family application in which you can create your family's relations and see how many relations there are for a certain person and see their family tree.
#### Theres some dummy data for testing application functionality, feel free to delete it all and enter your data.

## Running the application (cmd)
### First things first - clone to project.

### Backend
- /SummerUniversityNetgroup/SummerUniversityProject/WebApp> ```dotnet run```
- If curious, razor pages available at https://localhost:5001/
	
### Frontend
- /SummerUniversityNetgroup/SummerUniversityProjectClient> ```npm install```
- /SummerUniversityNetgroup/SummerUniversityProjectClient> ```npm start```
- Open your favourite web browser at http://localhost:8080/

### 10 ärireeglit (10 business rules)
- Eesnimi 1 kuni 128 tähemärki	*(First name 1-128 characters)*
- Perekonnanimi 1 kuni 128 tähemärki *(Last name 1-128 characters)*
- Vanus mitte alla 0 ega suurem kui 122 aastat *(Age not below 0 or higher than 122)*
- Abikaasa lisamisel lisatakse abikaasa ka teisele inimesele *(Adding spouse to one person will add spouse to the other one)*
- Enda lisamine endale mitte lubatud *(Not allowed to add yourself as a relation to yourself)*
- Kustutamise korral hoiatus *(Warning on delete)*
- Inimese kustutamisel kustutatakse ka tema sidemed *(Deleting a person will delete all his relations or those which are associated with him)*
- Isa/Ema ei saa olla noorem kui nende laps *(Parent can't be older than their child)*
- Vanaisa/Vanaema ei saa olla noorem kui nende lapselaps *(Grandparents can't be younger than their grandchildren)*
- Vanaema/Vanaisa lisamisel lisatakse lapselaps antud inimesele *(By adding grandparent to a person, the grandparent gets a grandchild added too)*

### Domain
![Image of domain schema](https://i.imgur.com/jbL8VHE.jpg)
