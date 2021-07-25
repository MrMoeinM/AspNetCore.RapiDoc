mkdir content

git clone https://github.com/highlightjs/cdn-release.git ./highlight.js
cd highlight.js
git checkout eb135c0221d2e7b
cp ./build/highlight.min.js ../content/highlight.min.js
cp ./build/styles/default.min.css ../content/default.min.css
cd ../
rm -rf ./highlight.js

npm install
cp ./node_modules/rapidoc/dist/rapidoc-min.js ./content/rapidoc-min.js
rm -rf ./node_modules