function dispatchEventWrapper(evt){
    
    window.dispatchEvent(new CustomEvent(evt))
}