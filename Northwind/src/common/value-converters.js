export class UpperValueConverter {
    toView(value){
        return value && value.toUpperCase();
    }
}

export class LowerValueConverter {
    toView(value, config, check){
        //console.log(check)
        return value && value.toLowerCase(); // + config.someProp;
    }
    //fromView(value) {
    //    return "Overridden!";
    //}
}