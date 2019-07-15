import resolve from 'rollup-plugin-node-resolve';
import pkg from './package.json';
 
export default {
    input: 'dist/app.js',
    output:{
        file: pkg.main,
        format: 'umd',
        name: 'app',
        sourcemap:"inline"
    },
    plugins: [
        resolve({
            jsnext: true,
            main: true,
            module: true
        })
    ],
   
};