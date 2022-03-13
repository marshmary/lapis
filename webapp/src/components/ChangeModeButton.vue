<script setup>
import { useDarkMode } from "@/store/useDarkMode";

const darkMode = useDarkMode();

const handleChangeToggle = () => {
    darkMode.setDarkMode(!darkMode.isDarkMode);
}
</script>

<template>
    <div class="toggle-wrapper">
        <label class="toggle">
            <input type="checkbox" :checked="darkMode.isDarkMode" @change="handleChangeToggle" />
            <span class="toggler round d-flex justify-content-between align-items-center">
                <font-awesome-icon icon="moon" size="sm" style="margin-left: 3px; color: white;" />
                <font-awesome-icon icon="sun" size="sm" style="margin-right: 3px; color: white;" />
            </span>
        </label>
    </div>
</template>

<style scoped>
.toggle-wrapper {
    height: 1.25rem;
}

.toggle {
    position: relative;
    display: inline-block;
    width: 2.375rem;
    height: 1.25rem;
}
.toggle input {
    opacity: 0;
    width: 0;
    height: 0;
}
.toggler {
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: #15202b;
    -webkit-transition: 0.4s;
    transition: 0.4s;
}
.toggler:before {
    position: absolute;
    content: "";
    height: 1rem;
    width: 1rem;
    left: 2px;
    bottom: 2px;
    background: #fff;
    -webkit-transition: 0.4s;
    transition: 0.4s;
}
input:checked + .toggler {
    background: #2196f3;
}
input:focus + toggler {
    box-shadow: 0 0 2px #2196f3;
}
input:checked + .toggler:before {
    -webkit-transform: translateX(17.5px);
    -ms-transform: translateX(17.5px);
    transform: translateX(17.5px);
}
.toggler.round {
    border-radius: 34px;
}
.toggler.round:before {
    border-radius: 50%;
}
</style>