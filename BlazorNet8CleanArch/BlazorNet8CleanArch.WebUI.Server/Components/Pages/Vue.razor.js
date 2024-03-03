import { createApp } from 'https://unpkg.com/petite-vue-csp@0.4.4/dist/petite-vue-csp.es.js'

const vueApp = {
    helloVue() {
        alert('Hello Blazor from Vue!')
    }
}

export function onLoad() {
    console.log('Loaded');
    createApp(vueApp).mount()
}

export function onUpdate() {
    console.log('Updated');
}

export function onDispose() {
    console.log('Disposed');
}

