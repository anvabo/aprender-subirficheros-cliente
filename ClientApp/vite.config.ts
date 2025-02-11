import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import mkcert from 'vite-plugin-mkcert'
import { resolve } from 'node:path'

// @ts-ignore
import appsettings from "../appsettings.json";
// @ts-ignore
import appsettingsDev from "../appsettings.development.json";
// @ts-ignore
import appsettingsPre from "../appsettings.staging.json";
// @ts-ignore
import appsettingsPro from "../appsettings.production.json";

const version = appsettings.App.Version;
//const idapp = appsettings.App.IdApp;

 // Pattern for CSS files
 const cssPattern = /\.css$/;
 // Pattern for image files
 const imagePattern = /\.(png|jpe?g|gif|svg|webp|avif)$/;

// https://vite.dev/config/
export default defineConfig({
  server: {
    https: true,
    port: 3000
    },
  plugins: [vue(), mkcert()],
  clearScreen: true,
//appType: 'mpa',
root: '../ClientApp',
publicDir: 'public',

build: {
  manifest: appsettings.Vite.Manifest,
  emptyOutDir: true,
  outDir: '../wwwroot',
  assetsDir: '',   

  rollupOptions: {
    input: {
      main: resolve(__dirname, 'index.html'),
    },
    output: {
      
      // Generar nombres de archivo con la version
      entryFileNames: `assets/main.${version}.js`,
      chunkFileNames: `assets/[name].${version}.js`,



      assetFileNames: (info) => {
        //console.log("info", info.name);
        if (info.name) {
          // If the file is a CSS file, save it to the css folder
          if (cssPattern.test(info.name)) {
            return `assets/[name].${version}.[ext]`;
          }
          // If the file is an image file, save it to the images folder
          if (imagePattern.test(info.name)) {
            return 'assets/images/[name][extname]';
          }

          // If the file is any other type of file, save it to the assets folder 
          return 'assets/[name][extname]';
        } else {
          // If the file name is not specified, save it to the output directory
          return '[name][extname]';
        }
      },
    },
  },
},

resolve: {
  alias: {
  '@': '/src'
  }
}
  
})
