// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

// let baseUrlAssets = window['baseUrlAssets'] || '';
let originalUrl = window.location.origin;

export const environment = {
  production: false,
  googleClientId: "482746260192-ra45mhi7igjuk1s6acneanmdqnn5vlip.apps.googleusercontent.com",
  googleClientSecret: "1dEl-xZoFj50p6cRx45TJiUa",
  API_BASE_URL: originalUrl
};


/*
ng build --configuration=test

 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
