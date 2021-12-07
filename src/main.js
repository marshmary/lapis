import { createApp } from 'vue'
import App from './App.vue'

// fontawesome
import { library } from '@fortawesome/fontawesome-svg-core'
import { faUserSecret, faClock, faDownload, faTags, faImage, faFileImage, faExclamationCircle, faCheckCircle } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import TheModal from "./components/TheModal.vue";

// bootstrap lib
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'

// router
import router from './router'

// store 
// import store from './store'

// add fontawesome icon to use
library.add(faUserSecret, faClock, faDownload, faTags, faImage, faFileImage, faExclamationCircle, faCheckCircle)

createApp(App)
    // .use(store)
    .use(router)
    .component("font-awesome-icon", <FontAwesomeIcon class="me-1" fixed-width></FontAwesomeIcon>)
    .component("the-modal", <TheModal></TheModal>)
    .mount('#app')
