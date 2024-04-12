#/bin/bash
mkdir -p /data/rocnsbe/media /data/rocnsbe/db /data/rocnsbe/logs
cp -rn /app/wwwroot/media/ /data/rocnsbe/
rm -rf /app/wwwroot/media/
cp -rn /app/umbraco/Data/Umbraco.sqlite* /data/rocnsbe/db
rm /app/umbraco/Data/Umbraco.sqlite*
cd /app
dotnet ROCNSBE.Website.dll