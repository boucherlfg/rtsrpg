set /p msg=Commit message:
git add --a
git commit -m "%msg%"
git push origin main