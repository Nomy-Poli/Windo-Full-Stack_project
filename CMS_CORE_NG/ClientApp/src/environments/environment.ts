// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

// let baseUrlAssets = window['baseUrlAssets'] || '';
let originalUrl = window.location.origin;


export const environment = {
  production: false,
  // googleClientId: "827381401565-s3mf6vk4vlffok2fqqafaub1np74v7ou.apps.googleusercontent.com",
  googleClientId: "827381401565-vklofajiq88oo9as833f80a7hh9j54mn.apps.googleusercontent.com",
  API_BASE_URL: originalUrl
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
