install docker desktop
install visual studio 2022

Cara nyalain Database: Tulis perintah Docker di terminal:
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=IDS3511p8" -p 1433:1433 --name sqlserver_local -d mcr.microsoft.com/mssql/server:2022-latest

Cara menjalankan API: tekan tombol Play di Visual Studio (pilih DocumentManagement.API)

Cara instalasi Frontend:
npm install --legacy-peer-deps (tulis di terminal folder frondend).

lalu tulis npm start