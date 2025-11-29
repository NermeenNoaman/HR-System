"use client"

import { useEffect, useState } from "react"
import Link from "next/link"
import { useRouter } from "next/navigation"
import { FiPlus, FiEdit2, FiTrash2, FiEye } from "react-icons/fi"

import { Card, CardHeader, CardTitle, CardDescription, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
  getAllEmployees,
  createEmployee,
  updateEmployee,
  deleteEmployee,
} from "@/lib/api"

export default function EmployeesPage() {
  const router = useRouter()
  const [role, setRole] = useState("")
  const [employees, setEmployees] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState("")
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [selectedEmployee, setSelectedEmployee] = useState(null)
  const [isFormOpen, setIsFormOpen] = useState(false)

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    position: "",
  })

  useEffect(() => {
    if (typeof window !== "undefined") {
      const userRole = localStorage.getItem("role")
      setRole(userRole || "")

      if (!userRole) {
        router.push("/login")
        return
      }

      // Only Admin and HR can manage employees
      if (!(userRole === "admin" || userRole === "HR")) {
        router.push("/dashboard")
        return
      }

      fetchEmployees()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [router])

  const fetchEmployees = async () => {
    try {
      setLoading(true)
      setError("")
      const { data } = await getAllEmployees()
      setEmployees(Array.isArray(data) ? data : [])
      console.log(data)
    } catch (err) {
      console.error("Failed to fetch employees:", err)
      setError("Failed to load employees. Please try again.")
    } finally {
      setLoading(false)
    }
  }

  const resetForm = () => {
    setFormData({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      position: "",
    })
    setSelectedEmployee(null)
  }

  const handleChange = (e) => {
    const { name, value } = e.target
    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setIsSubmitting(true)
    setError("")

    try {
      if (selectedEmployee) {
        await updateEmployee(selectedEmployee.id || selectedEmployee.employeeId, {
          ...selectedEmployee,
          ...formData,
        })
      } else {
        await createEmployee(formData)
      }

      resetForm()
      setIsFormOpen(false)
      await fetchEmployees()
    } catch (err) {
      console.error("Failed to save employee:", err)
      setError("Failed to save employee. Please check the data and try again.")
    } finally {
      setIsSubmitting(false)
    }
  }

  const handleEdit = (employee) => {
    setSelectedEmployee(employee)
    setIsFormOpen(true)
    setFormData({
      firstName: employee.firstName || "",
      lastName: employee.lastName || "",
      email: employee.email || "",
      phoneNumber: employee.phoneNumber || "",
      position: employee.position || "",
    })
  }

  const handleDelete = async (employeeId) => {
    if (!confirm("Are you sure you want to delete this employee?")) return

    try {
      await deleteEmployee(employeeId)
      await fetchEmployees()
    } catch (err) {
      console.error("Failed to delete employee:", err)
      setError("Failed to delete employee. Please try again.")
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-purple-900 via-blue-900 to-purple-900 p-6">
      <div className="max-w-7xl mx-auto space-y-8">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-4xl font-bold text-cyan-400 mb-2">Employees</h1>
            <p className="text-gray-300">
              Manage employee records. Only Admin and HR can create, update, or delete employees.
            </p>
          </div>
          <Button
            type="button"
            className="inline-flex items-center gap-2 bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white"
            onClick={() => {
              resetForm()
              setIsFormOpen(true)
            }}
          >
            <FiPlus className="w-4 h-4" />
            New Employee
          </Button>
        </div>

        {error && (
          <Card className="border-red-700 bg-red-900/20">
            <CardContent className="p-4">
              <p className="text-sm text-red-400">{error}</p>
            </CardContent>
          </Card>
        )}

        {/* Employees List - Full Width */}
        <Card className="bg-gradient-to-br from-gray-800 to-gray-900 border-gray-700">
          <CardHeader>
            <CardTitle className="text-white">Employee List</CardTitle>
            <CardDescription className="text-gray-400">
              Click the eye icon to view all employee details and fields returned by the API.
            </CardDescription>
          </CardHeader>
          <CardContent>
            {loading ? (
              <div className="flex items-center justify-center py-12">
                <p className="text-sm text-gray-400">Loading employees...</p>
              </div>
            ) : employees.length === 0 ? (
              <div className="flex items-center justify-center py-12">
                <p className="text-sm text-gray-400">No employees found.</p>
              </div>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full">
                  <thead>
                    <tr className="border-b border-gray-700 bg-gray-800/50">
                      <th className="py-4 px-6 text-left text-sm font-semibold text-gray-300">Name</th>
                      <th className="py-4 px-6 text-left text-sm font-semibold text-gray-300">Email</th>
                      <th className="py-4 px-6 text-left text-sm font-semibold text-gray-300">Phone</th>
                      <th className="py-4 px-6 text-right text-sm font-semibold text-gray-300">Actions</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-gray-700">
                    {employees.map((emp) => (
                      <tr 
                        key={emp.id || emp.employeeId} 
                        className="hover:bg-gray-800/50 transition-colors duration-150"
                      >
                        <td className="py-4 px-6">
                          <div className="font-medium text-white">
                            {((emp.firstName || "") + " " + (emp.lastName || "")).trim() || 
                             emp.name || 
                             emp.fullName || 
                             "-"}
                          </div>
                        </td>
                        <td className="py-4 px-6">
                          <span className="text-gray-300">{emp.email || emp.emailAddress || "-"}</span>
                        </td>
                        <td className="py-4 px-6">
                          <span className="text-gray-300">{emp.phoneNumber || emp.phone || emp.mobile || "-"}</span>
                        </td>
                        <td className="py-4 px-6">
                          <div className="flex justify-end gap-2">
                            <Link
                              href={`/dashboard/employees/${emp.id || emp.employeeId}`}
                              className="inline-flex items-center justify-center rounded-md border border-gray-600 bg-gray-700 px-3 py-1.5 text-xs font-medium text-gray-300 hover:bg-gray-600 hover:border-gray-500 transition-colors"
                              title="View all employee details"
                            >
                              <FiEye className="w-3.5 h-3.5 mr-1.5" />
                              View
                            </Link>
                            <Button
                              type="button"
                              variant="outline"
                              size="sm"
                              className="h-7 px-3 text-xs border-gray-600 text-gray-300 hover:bg-gray-700"
                              onClick={() => handleEdit(emp)}
                            >
                              <FiEdit2 className="w-3.5 h-3.5 mr-1.5" />
                              Edit
                            </Button>
                            <Button
                              type="button"
                              variant="destructive"
                              size="sm"
                              className="h-7 px-3 text-xs"
                              onClick={() => handleDelete(emp.id || emp.employeeId)}
                            >
                              <FiTrash2 className="w-3.5 h-3.5 mr-1.5" />
                              Delete
                            </Button>
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </CardContent>
        </Card>

        {/* Create / Edit Form */}
        {isFormOpen && (
          <Card className="bg-gradient-to-br from-gray-800 to-gray-900 border-gray-700">
            <CardHeader>
              <CardTitle className="text-white">{selectedEmployee ? "Edit Employee" : "New Employee"}</CardTitle>
              <CardDescription className="text-gray-400">
                {selectedEmployee
                  ? "Update the selected employee's information."
                  : "Create a new employee record."}
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="space-y-1">
                    <Label htmlFor="firstName" className="text-gray-300">First Name</Label>
                    <Input
                      id="firstName"
                      name="firstName"
                      value={formData.firstName}
                      onChange={handleChange}
                      required
                      className="bg-gray-700 border-gray-600 text-white placeholder:text-gray-400 focus:border-cyan-400"
                    />
                  </div>
                  <div className="space-y-1">
                    <Label htmlFor="lastName" className="text-gray-300">Last Name</Label>
                    <Input
                      id="lastName"
                      name="lastName"
                      value={formData.lastName}
                      onChange={handleChange}
                      required
                      className="bg-gray-700 border-gray-600 text-white placeholder:text-gray-400 focus:border-cyan-400"
                    />
                  </div>
                </div>

                <div className="space-y-1">
                  <Label htmlFor="email" className="text-gray-300">Email</Label>
                  <Input
                    id="email"
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                    className="bg-gray-700 border-gray-600 text-white placeholder:text-gray-400 focus:border-cyan-400"
                  />
                </div>

                <div className="space-y-1">
                  <Label htmlFor="phoneNumber" className="text-gray-300">Phone Number</Label>
                  <Input
                    id="phoneNumber"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    className="bg-gray-700 border-gray-600 text-white placeholder:text-gray-400 focus:border-cyan-400"
                  />
                </div>

                <div className="space-y-1">
                  <Label htmlFor="position" className="text-gray-300">Position</Label>
                  <Input
                    id="position"
                    name="position"
                    value={formData.position}
                    onChange={handleChange}
                    className="bg-gray-700 border-gray-600 text-white placeholder:text-gray-400 focus:border-cyan-400"
                  />
                </div>

                <div className="flex justify-end gap-2 pt-2">
                  <Button
                    type="button"
                    variant="outline"
                    onClick={() => {
                      resetForm()
                      setIsFormOpen(false)
                    }}
                    disabled={isSubmitting}
                    className="border-gray-600 text-gray-300 hover:bg-gray-700"
                  >
                    Cancel
                  </Button>
                  <Button type="submit" disabled={isSubmitting} className="bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700">
                    {isSubmitting ? "Saving..." : selectedEmployee ? "Update Employee" : "Create Employee"}
                  </Button>
                </div>
              </form>
            </CardContent>
          </Card>
        )}
      </div>
    </div>
  )
}
