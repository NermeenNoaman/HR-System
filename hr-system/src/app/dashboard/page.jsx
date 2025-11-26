"use client"

import { useEffect, useState } from "react"
import { useRouter } from "next/navigation"
import Link from "next/link"
import {
  FiBriefcase,
  FiMap,
  FiUsers,
  FiFolder,
  FiCalendar,
  FiDollarSign,
} from "react-icons/fi"
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from "@/components/ui/card"
import { cn } from "@/lib/utils"

export default function DashboardPage() {
  const router = useRouter()
  const [role, setRole] = useState("")
  const [username, setUsername] = useState("")

  useEffect(() => {
    if (typeof window !== "undefined") {
      const userRole = localStorage.getItem("role")
      const userName = localStorage.getItem("username")
      setRole(userRole || "")
      setUsername(userName || "")

      // Redirect to login if no role found
      if (!userRole) {
        router.push("/login")
      }
    }
  }, [router])

  const isAdmin = role === "admin"
  const isHR = role === "HR"
  const canView = isAdmin || isHR

  if (!canView) {
    return (
      <div className="max-w-4xl mx-auto">
        <Card className="border-red-200 bg-red-50/50">
          <CardContent className="p-6">
            <p className="text-destructive font-medium">
              You don't have permission to access the dashboard.
            </p>
          </CardContent>
        </Card>
      </div>
    )
  }

  const dashboardItems = [
    {
      title: "Company Profiles",
      description: "Manage company profile information",
      href: "/dashboard/company-profiles",
      icon: FiBriefcase,
      gradient: "from-blue-500 to-cyan-500",
      available: isAdmin || isHR,
    },
    {
      title: "Company Branches",
      description: "Manage company branch information",
      href: "/dashboard/branches",
      icon: FiMap,
      gradient: "from-purple-500 to-pink-500",
      available: isAdmin || isHR,
    },
    {
      title: "Employees",
      description: "Manage employee information",
      href: "/dashboard/employees",
      icon: FiUsers,
      gradient: "from-indigo-500 to-blue-500",
      available: isAdmin || isHR,
    },
    {
      title: "Departments",
      description: "Manage department information",
      href: "/dashboard/departments",
      icon: FiFolder,
      gradient: "from-violet-500 to-purple-500",
      available: isAdmin || isHR,
    },
    {
      title: "Attendance",
      description: "View and manage attendance records",
      href: "/dashboard/attendance",
      icon: FiCalendar,
      gradient: "from-blue-600 to-indigo-600",
      available: isAdmin || isHR,
    },
    {
      title: "Payroll",
      description: "Manage payroll information",
      href: "/dashboard/payroll",
      icon: FiDollarSign,
      gradient: "from-purple-600 to-violet-600",
      available: isAdmin || isHR,
    },
  ]

  return (
    <div className="max-w-7xl mx-auto space-y-8">
      {/* Header */}
      <div className="space-y-2">
        <h1 className="text-5xl font-bold bg-gradient-to-r from-blue-600 via-purple-600 to-blue-600 bg-clip-text text-transparent">
          Dashboard
        </h1>
        <p className="text-lg text-gray-600">
          Welcome back{username ? `, ${username}` : ""}! You're logged in as{" "}
          <span className="font-semibold text-purple-600">{role}</span>
        </p>
      </div>

      {/* Dashboard Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {dashboardItems
          .filter((item) => item.available)
          .map((item) => {
            const Icon = item.icon
            return (
              <Link key={item.href} href={item.href}>
                <Card className="h-full group hover:shadow-2xl transition-all duration-300 border-2 hover:border-transparent overflow-hidden relative">
                  <div
                    className={cn(
                      "absolute inset-0 bg-gradient-to-br opacity-0 group-hover:opacity-100 transition-opacity duration-300",
                      item.gradient
                    )}
                  />
                  <CardHeader className="relative z-10">
                    <div className="flex items-start gap-4">
                      <div
                        className={cn(
                          "p-3 rounded-xl bg-gradient-to-br text-white shadow-lg",
                          item.gradient
                        )}
                      >
                        <Icon size={24} />
                      </div>
                      <div className="flex-1">
                        <CardTitle className="text-xl group-hover:text-white transition-colors duration-300">
                          {item.title}
                        </CardTitle>
                        <CardDescription className="group-hover:text-white/90 transition-colors duration-300 mt-1">
                          {item.description}
                        </CardDescription>
                      </div>
                    </div>
                  </CardHeader>
                  <CardContent className="relative z-10">
                    <div
                      className={cn(
                        "inline-flex items-center text-sm font-medium text-transparent bg-clip-text bg-gradient-to-r group-hover:text-white transition-colors duration-300",
                        item.gradient
                      )}
                    >
                      View Details →
                    </div>
                  </CardContent>
                </Card>
              </Link>
            )
          })}
      </div>

      {/* Stats or Quick Info Section */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <Card className="bg-gradient-to-br from-blue-500 to-cyan-500 text-white border-0 shadow-xl">
          <CardHeader>
            <CardTitle className="text-white">System Status</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-blue-100">All systems operational</p>
          </CardContent>
        </Card>
        <Card className="bg-gradient-to-br from-purple-500 to-pink-500 text-white border-0 shadow-xl">
          <CardHeader>
            <CardTitle className="text-white">Active Users</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-purple-100">Role: {role}</p>
          </CardContent>
        </Card>
        <Card className="bg-gradient-to-br from-indigo-500 to-blue-500 text-white border-0 shadow-xl">
          <CardHeader>
            <CardTitle className="text-white">Quick Access</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-indigo-100">Navigate using the sidebar</p>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}

