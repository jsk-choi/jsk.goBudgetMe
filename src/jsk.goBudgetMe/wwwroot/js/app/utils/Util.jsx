const Util = {
    log (title, log, indent = 0) {
        if (typeof title == 'object'){
            console.log(title);
        }
        else {
            console.log(title, JSON.stringify(log, null, indent));
        }
    }
};

export default Util;