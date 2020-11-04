<template>
    <div id="app">
        <b-container class="bv-example-row">
            <b-row align-v="center">
                <b-col>
                    <h2 class="mt-4 ml-4">Sales Department</h2>
                </b-col>
            </b-row>
            <hr />
            <b-row>
                <b-col cols="9">
                    <b-row >
                        <rota-generator></rota-generator>
                    </b-row>
                    <b-row>
                        <shift-menu></shift-menu>
                    </b-row>
                </b-col>
                <b-col><employee-list :employees="employees"></employee-list></b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script>
    import axios from 'axios';
    import EmployeeList from './components/EmployeeList.vue';
    import RotaGenerator from './components/RotaGenerator.vue';
    import ShiftMenu from './components/ShiftMenu.vue';
    import 'bootstrap/dist/css/bootstrap.css'
    import 'bootstrap-vue/dist/bootstrap-vue.css'

    export default {
        name: 'app',
        components: {
            EmployeeList,
            RotaGenerator,
            ShiftMenu
        },
        data() {
            return {
                employees: [],
            }
        },
        mounted() {
            axios
                .get('https://localhost:44352/api/employees/', {
                    headers: {
                        'Access-Control-Allow-Origin': true,
                    }
                })
                .then(response => (this.employees = response.data))
        }
    };
</script>

<style>
</style>




