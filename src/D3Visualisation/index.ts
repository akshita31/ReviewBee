import {select} from "d3";

// import * as _d3 from "d3";

// declare global {
//     const d3: typeof _d3;
// }

// export function appendSpan(){
//     d3.select("div.myclass").append("span");
// }
export function appendSpan(){
    select("div.myClass").append("span");
}
