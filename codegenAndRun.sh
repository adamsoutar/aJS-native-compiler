# Backup clean copy of template code
mv ./ajs-test/Program.cs ./ajs-test/Program.cs.bak

cd ajs-compiler
dotnet run 2>&1> ../ajs-test/Program.cs && \
cd ../ajs-test && \
cat Program.cs && \
echo "-=-" && \
dotnet run && \
cd ../

# Restore backup
cd ajs-test
mv ./Program.cs.bak ./Program.cs
