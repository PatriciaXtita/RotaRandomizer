<template>
    <div id="employee-list">
        <b-container class="bv-example-row">
            <b-row align-v="center">
                <b-col>
                    <h3>Employees</h3>
                </b-col>
            </b-row>
            <b-table class="text-center" striped small bordered :items=this.employees></b-table>
        </b-container>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'employee-list',
        data() {
            return {
                employees: [],
            }
        },
        mounted() {
            axios
                .get('https://localhost:44352/api/employees/')
                .then(response => (this.employees = response.data))
        },
    }
</script>

<style scoped></style>