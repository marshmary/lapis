import { createApp } from "vue";
import App from "./App.vue";

import { VueMasonryPlugin } from "vue-masonry";

import { ObserveVisibility } from "vue-observe-visibility";

// Tags input
import VoerroTagsInput from "@voerro/vue-tagsinput";
import "@voerro/vue-tagsinput/dist/style.css";

// fontawesome
import { library } from "@fortawesome/fontawesome-svg-core";
import {
    faUserSecret,
    faClock,
    faDownload,
    faTags,
    faImage,
    faFileImage,
    faExclamationCircle,
    faCheckCircle,
    faArrowCircleUp,
    faTrash,
    faBars,
    faSearch,
    faTimes,
    faCheck,
    faArrowUp,
    faSun,
    faMoon,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

// bootstrap lib
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

// router
import router from "./router";

// store
import { createPinia } from "pinia";

// add fontawesome icon to use
library.add(
    faUserSecret,
    faClock,
    faDownload,
    faTags,
    faImage,
    faFileImage,
    faExclamationCircle,
    faCheckCircle,
    faArrowCircleUp,
    faTrash,
    faBars,
    faSearch,
    faTimes,
    faCheck,
    faArrowUp,
    faSun,
    faMoon
);

import Cache from "./helpers/cacheHandler";

Cache.init();

createApp(App)
    .use(createPinia())
    .use(router)
    .directive("observe-visibility", {
        beforeMount: (el, binding, vnode) => {
            vnode.context = binding.instance;
            ObserveVisibility.bind(el, binding, vnode);
        },
        updated: ObserveVisibility.update,
        unmounted: ObserveVisibility.unbind,
    })
    .use(VueMasonryPlugin)
    .component(
        "font-awesome-icon",
        <FontAwesomeIcon fixed-width></FontAwesomeIcon>
    )
    .component("tags-input", VoerroTagsInput)
    .mount("#app");
