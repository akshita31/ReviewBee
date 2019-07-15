import resolve from 'rollup-plugin-node-resolve';
import pkg from './package.json';
import typescript from 'rollup-plugin-typescript2';
 
export default {
    input: 'src/app.ts',    
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
        }),
        typescript(/*{ plugin options }*/)
    ],
   
};