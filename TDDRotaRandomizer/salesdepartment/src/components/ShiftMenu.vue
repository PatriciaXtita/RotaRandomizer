<template>
    <div id="shift-menu" class="w-100">
        <b-container class="bv-example-row">
            <b-row>
                <b-col align="center">
                    <h4 class="mt-4">Today's Shift</h4>
                </b-col>
            </b-row>
            <hr />
            <b-row>
                <b-container class="w-100">
                    <b-row v-for="shift in shifts" :key="shift.index">
                        <b-col class="w-100" align="center">
                            <h6 v-if="shift.shiftType == 1">Morning</h6>
                            <h6 v-if="shift.shiftType == 2">Afternoon</h6>
                            {{shift.shiftEmployee}}
                        </b-col>
                    </b-row>
                </b-container>
            </b-row>
        </b-container>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'shift-menu',
        data() {
            return {
                shifts: [],
            }
        },
        mounted() {
            axios
                .get('https://localhost:44352/api/shifts/today')
                .then(response => (this.shifts = response.data))
        }
    }
</script>

<style scoped></style>