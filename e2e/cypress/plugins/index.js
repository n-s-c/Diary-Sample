/// <reference types="cypress" />
// ***********************************************************
// This example plugins/index.js can be used to load plugins
//
// You can change the location of this file or turn off loading
// the plugins file with the 'pluginsFile' configuration option.
//
// You can read more here:
// https://on.cypress.io/plugins-guide
// ***********************************************************

// This function is called when a project is opened or re-opened (e.g. due to
// the project's config changing)

/**
 * @type {Cypress.PluginConfig}
 */
const fs = require("fs");

module.exports = (on, config) => {
  on("after:screenshot", (details) => {
    console.log(details); // print all details to terminal
    const newPath = "cypress/screenshots/" + details.name + ".png";

    return new Promise((resolve, reject) => {
      // fs.rename moves the file to the existing directory 'new/path/to'
      // and renames the image to 'screenshot.png'
      fs.rename(details.path, newPath, (err) => {
        if (err) return reject(err);

        // because we renamed and moved the image, resolve with the new path
        // so it is accurate in the test results
        resolve({ path: newPath });

        // delete folder
        fs.rmdir("cypress/screenshots/" + details.specName, (err) => {
          if (err) return reject(err);

          console.log("ディレクトリを削除しました");
        });
      });
    });
  });
};
