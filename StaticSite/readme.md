# Web App Starting Point
This package is an initial setup of either a react app or just typescript.  Meant to help get things 'started' faster.

It includes the starting point of Typescript, React, and Webpack, with minification / mapping, sample files and a bundle.

## First Time Startup
Make sure you have [npm installed](https://www.npmjs.com/get-npm) on your computer globally.

At the root of this folder, open a powershell and run `npm install` which will restore the `node_modules` folder.

## Modifying / Removing Applications
This baseline has 2 sample applications (1 Typescript Hello World and 1 React Hello World), along with a webpack.config.js to bundle them.

If you want to start a new React app or Typescript library package, you can simply copy and paste the react/sampleapp or typescript/samplehelper folders, or create your own.

Each application has 3 configuration points that you'll want to adjust if you add, remove, or rename:

1. `tsconfig.json` => `include` : Add or adjust the paths on where .ts or .tsx files are located so typescript can see them.
1. `package.json` => `scripts` => `webpack`/`webpack-dev` : These are the "Build Everything" and "Watch Everything" commands (see `Build and Watch Commands` below).  Adjust these to add/remove/rename the webpack.config.js files for your applications.  `&&` chains things together.
1. `package.json` => `scripts` => `webpack-modulename`/`webpack-modulename-dev` : These are the "Build this Application" and "Watch this Application" commands that can be run individually if you only want to build or watch for changes on a specific application.
1. `webpack.config.js` => `entry` / `output filename` : The webpack.config.js for each application determines where the starting point is to build and the outputted file, adjust these as needed.

Optional (if bundling)
1. `./webpack.config.js` => `entry` : the root webpack.config.js (if you keep it) is the bundling, so add any new applications or remove them from the entry point


## Build and Watch Commands
When you bring up this project to work on it, in a terminal, run:

`npm run webpack-dev` to watch all 3 webpack.config.js for changes (when you update a file, it rebuilds the dist).  This uses `concurrently` which allows multiple concurrent commands, since watch statements 'freeze' the terminal as it loops and watches.

`npm run webpack` to build all 3 webpack.config.js

You can also optionally run each one individually with the following commands, feel free to adjust, add, etc.
`npm run webpack-bundle` to build the bundle webpack
`npm run webpack-bundle-dev` to watch on the bundle
`npm run webpack-react` to build only the react app
`npm run webpack-react-dev` to watch only the react app
`npm run webpack-typescript` to build only the typescript sample
`npm run webpack-typescript-dev` to watch only on the typescript sample

## tsconfig.json
This configures the type script, you will want to update this to point to any new module folders so it compiles it.

## webpack.config.js files (3 included)
These are the 'compilers' of your react/typescript, this will take the ts file and bring it into a browser usable file.  You'll want to modify this for each new module, or delete if you don't need them.

It uses `source-map` for it's creation of source mapping, and `TerserPlugin` to minify.

The `webpack.config.js` at the root is a bundler which bundles them all together into a single bundle.min.js file.  Otherwise each application has it's own `webpack.config.js`
