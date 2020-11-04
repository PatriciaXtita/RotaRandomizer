<template>
    <div id="employee-list">
        <b-container class="bv-example-row">
            <b-row align-v="center">
                <b-col>
                    <h3>Employees</h3>
                </b-col>
            </b-row>
            <b-table striped small bordered :items=this.employees></b-table>
        </b-container>
    </div>
</template>

<script>
    export default {
        name: 'employee-list',
        props: {
            employees: Array,
        },
    }
</script>

<style scoped></style>