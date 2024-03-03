import { createApp } from 'https://unpkg.com/petite-vue-csp@0.4.4/dist/petite-vue-csp.es.js'

const vueApp = {
    // exposed to all expressions
    currentCount: 0,
    // getters
    get plusOne() {
        return this.currentCount + 1
    },
    // methods
    incrementCount() {
        this.currentCount++;
    }
}

export function onLoad() {
    console.log('Loaded');
    createApp(vueApp).mount()
}

export function onUpdate() {
    console.log('Updated');   
}


