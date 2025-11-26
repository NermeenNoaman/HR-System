import api from "./axios";
import axios from "axios";

/* ======================
        AUTH
====================== */

// Register a new user
export const register = (data) => {
  return api.post("/Auth/register", data);
};

// Login user
export const login = (data) => {
  return api.post("/Auth/login", data);
};

// Refresh token (manual refresh if needed)
export const refreshToken = (refreshToken) => {
  return axios.post("http://localhost:5179/api/Auth/refresh-token", refreshToken, {
    headers: {
      "Content-Type": "text/plain",
    },
  });
};

/* ======================
    COMPANY PROFILE
====================== */

// Get all company profiles
export const getAllCompanyProfiles = () => {
  return api.get("/CompanyProfile");
};

// Get company profile by ID
export const getCompanyProfileById = (id) => {
  return api.get(`/CompanyProfile/${id}`);
};

// Create company profile
export const createCompanyProfile = (data) => {
  return api.post("/CompanyProfile", data);
};

// Update company profile
export const updateCompanyProfile = (id, data) => {
  return api.put(`/CompanyProfile/${id}`, data);
};

// Delete company profile (soft delete)
export const deleteCompanyProfile = (id) => {
  return api.delete(`/CompanyProfile/${id}`);
};

/* ======================
        BRANCH
====================== */

// Get all branches
export const getAllBranches = () => {
  return api.get("/Branch");
};

// Get branch by ID
export const getBranchById = (id) => {
  return api.get(`/Branch/${id}`);
};

// Get branches by company ID
export const getBranchesByCompanyId = (companyId) => {
  return api.get(`/Branch/company/${companyId}`);
};

// Create branch
export const createBranch = (data) => {
  return api.post("/Branch", data);
};

// Update branch
export const updateBranch = (id, data) => {
  return api.put(`/Branch/${id}`, data);
};

// Delete branch (soft delete)
export const deleteBranch = (id) => {
  return api.delete(`/Branch/${id}`);
};

/* ======================
    HR DEPARTMENT
====================== */

// Get all HR departments
export const getAllHRDepartments = () => {
  return api.get("/HRDepartment");
};

// Get HR departments by branch ID
export const getHRDepartmentsByBranchId = (branchId) => {
  return api.get(`/HRDepartment/branch/${branchId}`);
};

// Create HR department
export const createHRDepartment = (data) => {
  return api.post("/HRDepartment", data);
};

// Update HR department
export const updateHRDepartment = (id, data) => {
  return api.put(`/HRDepartment/${id}`, data);
};

// Delete HR department (soft delete)
export const deleteHRDepartment = (id) => {
  return api.delete(`/HRDepartment/${id}`);
};

/* ======================
        USER
====================== */

// Get all users
export const getAllUsers = () => {
  return api.get("/User");
};

// Get user by ID
export const getUserById = (id) => {
  return api.get(`/User/${id}`);
};

// Update user role
export const updateUserRole = (id, role) => {
  return api.put(`/User/${id}/role`, role, {
    headers: {
      "Content-Type": "text/plain",
    },
  });
};

// Delete user
export const deleteUser = (id) => {
  return api.delete(`/User/${id}`);
};

/* ======================
        EMPLOYEE
====================== */

// Create a new employee
// Authorization: Admin, HR
export const createEmployee = (data) => {
  return api.post("/Employee", data);
};

// Get all employees (view-only)
// Authorization: Admin, HR
export const getAllEmployees = () => {
  return api.get("/Employee");
};

// Get employee by ID
// Authorization: Admin, HR, Employee
// - Admin/HR can view any employee
// - Employee can view only their own data (enforced by backend using JWT)
export const getEmployeeById = (id) => {
  return api.get(`/Employee/${id}`);
};

// Update employee by ID
// Authorization: Admin, HR
// Condition: Backend should ensure the ID in route matches ID in body
export const updateEmployee = (id, data) => {
  return api.put(`/Employee/${id}`, data);
};

// Delete employee by ID
// Authorization: Admin
export const deleteEmployee = (id) => {
  return api.delete(`/Employee/${id}`);
};

